using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WaltCapitalManagementWebAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly CommonHelper _commonHelper;


        public ExceptionFilter(CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                CommonResponse commonReponse = new CommonResponse();
                context.Result = new JsonResult(commonReponse);

                var item = context.Exception;
                _commonHelper.AddLog(item.ToString());

            }
            catch (Exception ex)
            {
                _commonHelper.AddLog(ex.ToString());
            }
        }
    }
}
