using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class CurrencyImpl : ICurrency
    {
        private readonly CurrencyBLL _iCurrencyBLL;

        public CurrencyImpl(CurrencyBLL iCurrencyBLL)
        {
            _iCurrencyBLL = iCurrencyBLL;
        }

        public CommonResponse GetAllCurrency()
        {
            return _iCurrencyBLL.GetAllCurrency();
        }

        public CommonResponse AddCurrency(AddCurrencyReqDTO addCurrencyReqDTO)
        {
            return _iCurrencyBLL.AddCurrency(addCurrencyReqDTO);
        }

    }
}
