using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IRegion
    {
        public CommonResponse GetAllCountry();
        public CommonResponse GetAllStateByCountryID(GetStateReqDTO getStateReqDTO);
        public CommonResponse GetAllCityByStateID(GetCityReqDTO getCityReqDTO);
        public CommonResponse GetAllCountryCustom();
        public CommonResponse GetAllStateCustomByCountryId(GetAllStateCustomReqDTO getAllStateCustomReqDTO);
        public CommonResponse GetAllCityCustomByStateId(GetAllCityCustomReqDTO getAllCityCustomReqDTO);
        public CommonResponse AddCountryCustom(AddCountryCustomReqDTO addCountryCustomReqDTO);
        public CommonResponse AddStateCustom(AddStateCustomReqDTO addStateCustomReqDTO);
        public CommonResponse AddCityCustom(AddCityCustomReqDTO addCityCustomReqDTO);
        public CommonResponse UpdateCountryCustom(UpdateCountryCustomReqDTO updateCountryCustomReqDTO);
        public CommonResponse DeleteCountryCustom(DeleteCountryCustomReqDTO deleteCountryCustomReqDTO);
        public CommonResponse UpdateStateCustom(UpdateStateCustomReqDTO updateStateCustomReqDTO);
        public CommonResponse DeleteStateCustom(DeleteStateCustomReqDTO deleteStateCustomReqDTO);
        public CommonResponse GetCountryByCountryId(GetCountryByCountryIdReqDTO getByCountryIdReqDTO);
        public CommonResponse GetStateByStateId(GetStateByStateIdReqDTO getByStateIdReqDTO);
        public CommonResponse GetCityByCityId(GetCityByCityIdReqDTO getByCityIdReqDTO);
        public CommonResponse AddCustomCity(AddCustomCityReqDTO addCustomCityReqDTO);
        public CommonResponse UpdateCustomCity(UpdateCustomCityReqDTO updateCustomCityReqDTO);
        public CommonResponse DeleteCustomCity(DeleteCustomCityReqDTO deleteCustomCityReqDTO);
    }
}
