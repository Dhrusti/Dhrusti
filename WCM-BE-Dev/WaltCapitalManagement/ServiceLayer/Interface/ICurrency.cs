using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface ICurrency
    {

        public CommonResponse GetAllCurrency();
        public CommonResponse AddCurrency(AddCurrencyReqDTO addCurrencyReqDTO);

    }
}
