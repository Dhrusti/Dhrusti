using DataLayer.Entities;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text;

namespace WaltCapitalManagementWebAPI.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonHelper _commonHelper;
        private readonly AuthRepo _authRepo;
        public AuthorizationFilter(IConfiguration configuration, WaltCapitalDBContext dbContext, CommonHelper commonHelper, AuthRepo authRepo)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _commonHelper = commonHelper;
            _authRepo = authRepo;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                bool isAllEncrypted = Convert.ToBoolean(_configuration["AllApiEncryptionSwitch"].ToString());
                if (isAllEncrypted)
                {
                    var request = context.HttpContext.Request;
                    var data = context.RouteData.Values["action"] as string; // To get method name of current request.

                    if (!string.IsNullOrEmpty(data) && data != "GetDecryption" && data != "GetEncryption" && data != "AddIdProof" && data != "AddAddressProof" && data != "AddProfilePhoto" && data != "UploadCSVDocument" && data != "UploadClientCSVDocument" && data != "UploadCSVDataDocument")

                    {
                        using (var reader = new StreamReader(request.Body))
                        {
                            var json = reader.ReadToEndAsync();
                            if (!string.IsNullOrEmpty(json.Result))
                            {
                                //1.get the value in perticular model
                                CommonResponse commonResponse = JsonConvert.DeserializeObject<CommonResponse>(json.Result);

                                //2.modify the value
                                //var decriptedFromJavascript = new AuthRepo(_configuration).DecryptString(commonResponse.Data);
                                var decriptedFromJavascript = _authRepo.DecryptString(commonResponse.Data);

                                //var decriptedFromJavascript = "{\"id\":0,\"name\":\"string\",\"data\":\"string\"}";
                                byte[] bytes = Encoding.ASCII.GetBytes(decriptedFromJavascript);
                                //3. add the value and update request
                                request.Body = new MemoryStream(bytes);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // new CommonHelper(_configuration,IHostingEnvironment ).AddLog("Exception :: " + ex.ToString());
            }
        }
    }
}
