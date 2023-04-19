using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace WebAPI.Filters
{
    public class ActionFilter : IActionFilter
    {

        private readonly IConfiguration _configuration;
        private readonly CommonHelper _commonHelper;
        public ActionFilter(IConfiguration configuration, CommonHelper commonHelper)
        {
            _configuration = configuration;
            _commonHelper = commonHelper;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

                if (controllerActionDescriptor != null)
                {
                    string JsonData = JsonConvert.SerializeObject(context.ActionArguments.Values);
                    JsonData = JsonData.Substring(1, JsonData.Length - 2);
                    context.HttpContext.Items["LogRequestBody"] = JsonData;
                }
            }
            catch (Exception) { }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            bool isAllEncrypted = Convert.ToBoolean(_configuration["CommonSwitches:AllApiEncryptionSwitch"].ToString());
            bool isLogEnabled = Convert.ToBoolean(_configuration["CommonSwitches:APILogSwitch"].ToString());

            if (isLogEnabled || isAllEncrypted)
            {
                var result = context.Result;
                if (result is ObjectResult json)
                {
                    var data = context.RouteData.Values["action"] as string; // To get method name of current request.
                    if (!string.IsNullOrEmpty(data) && data != "GetDecryption" && data != "GetEncryption")
                    {
                        CommonResponse commonResponse = (CommonResponse)json.Value ?? new CommonResponse();

                        dynamic jsonDataString1 = JsonConvert.SerializeObject(commonResponse, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                        if (isAllEncrypted)
                        {
                            if (commonResponse.StatusCode == HttpStatusCode.OK)
                            {
                                dynamic jsonDataString = JsonConvert.SerializeObject(commonResponse.Data, new JsonSerializerSettings
                                {
                                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                                });
                                commonResponse.Data = _commonHelper.EncryptString(jsonDataString);
                            }
                        }

                        if (isLogEnabled)
                        {
                            var requestBody = context.HttpContext.Items["LogRequestBody"] != null ? Convert.ToString(context.HttpContext.Items["LogRequestBody"]) : "";

                            var requestData = context.HttpContext.Request;
                            _commonHelper.AddActivityLog(requestData.Path, requestData.Method, jsonDataString1, requestBody);
                            //_commonHelper.AddActivityLog(context, jsonDataString1, requestBody);
                        }
                    }
                }
            }
        }
    }
}
