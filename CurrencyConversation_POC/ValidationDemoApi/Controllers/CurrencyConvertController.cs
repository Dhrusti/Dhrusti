using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Helper;

namespace ValidationDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConvertController : ControllerBase
    {
        private readonly CurrencyConvert _currencyconvertdata;
        public CurrencyConvertController(CurrencyConvert currencyconvertdata)
        {
            _currencyconvertdata = currencyconvertdata;
        }

        [HttpPost("CurrenConvert")]
        public IActionResult CurrencyConvert(CurrencyModel currencyModel)
        {
            var res = _currencyconvertdata.CurrencyConversion(currencyModel);
            return Ok(res);
        } 
 
    }
}
