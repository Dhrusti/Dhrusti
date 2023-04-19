using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class RegionImpl : IRegion
    {
        private readonly RegionBLL _regionBLL;

        public RegionImpl(RegionBLL regionBLL)
        {
            _regionBLL = regionBLL;
        }

        public CommonResponse GetAllCountry()
        {
            return _regionBLL.GetAllCountry();
        }
        public CommonResponse GetAllStateByCountryID(GetStateReqDTO getStateReqDTO)
        {
            return _regionBLL.GetAllStateByCountryID(getStateReqDTO);
        }
        public CommonResponse GetAllCityByStateID(GetCityReqDTO getCityReqDTO)
        {
            return _regionBLL.GetAllCityByStateID(getCityReqDTO);
        }
        public CommonResponse GetAllCountryCustom()
        {
            return _regionBLL.GetAllCountryCustom();
        }
        public CommonResponse GetAllStateCustomByCountryId(GetAllStateCustomReqDTO getAllStateCustomReqDTO)
        {
            return _regionBLL.GetAllStateCustomByCountryId(getAllStateCustomReqDTO);
        }
        public CommonResponse GetAllCityCustomByStateId(GetAllCityCustomReqDTO getAllCityCustomReq)
        {
            return _regionBLL.GetAllCityCustomByStateId(getAllCityCustomReq);
        }
        public CommonResponse AddCountryCustom(AddCountryCustomReqDTO addCountryCustomReqDTO)
        {
            return _regionBLL.AddCountryCustom(addCountryCustomReqDTO);
        }
        public CommonResponse AddStateCustom(AddStateCustomReqDTO addStateCustomReqDTO)
        {
            return _regionBLL.AddStateCustom(addStateCustomReqDTO);
        }
        public CommonResponse AddCityCustom(AddCityCustomReqDTO addCityCustomReqDTO)
        {
            return _regionBLL.AddCityCustom(addCityCustomReqDTO);
        }
        public CommonResponse UpdateCountryCustom(UpdateCountryCustomReqDTO updateCountryCustomReqDTO)
        {
            return _regionBLL.UpdateCountryCustom(updateCountryCustomReqDTO);
        }
        public CommonResponse DeleteCountryCustom(DeleteCountryCustomReqDTO deleteCountryCustomReqDTO)
        {
            return _regionBLL.DeleteCountryCustom(deleteCountryCustomReqDTO);
        }
        public CommonResponse UpdateStateCustom(UpdateStateCustomReqDTO updateStateCustomReqDTO)
        {
            return _regionBLL.UpdateStateCustom(updateStateCustomReqDTO);
        }
        public CommonResponse DeleteStateCustom(DeleteStateCustomReqDTO deleteStateCustomReqDTO)
        {
            return _regionBLL.DeleteStateCustom(deleteStateCustomReqDTO);
        }
        public CommonResponse GetCountryByCountryId(GetCountryByCountryIdReqDTO getByCountryIdReqDTO)
        {
            return _regionBLL.GetCountryByCountryId(getByCountryIdReqDTO);
        }
        public CommonResponse GetStateByStateId(GetStateByStateIdReqDTO getByStateIdReqDTO)
        {
            return _regionBLL.GetStateByStateId(getByStateIdReqDTO);
        }
        public CommonResponse GetCityByCityId(GetCityByCityIdReqDTO getByCityIdReqDTO)
        {
            return _regionBLL.GetCityByCityId(getByCityIdReqDTO);
        }
        public CommonResponse AddCustomCity(AddCustomCityReqDTO addCustomCityReqDTO)
        {
            return _regionBLL.AddCustomCity(addCustomCityReqDTO);
        }
        public CommonResponse UpdateCustomCity(UpdateCustomCityReqDTO updateCustomCityReqDTO)
        {
            return _regionBLL.UpdateCustomCity(updateCustomCityReqDTO);
        }
        public CommonResponse DeleteCustomCity(DeleteCustomCityReqDTO deleteCustomCityReqDTO)
        {
            return _regionBLL.DeleteCustomCity(deleteCustomCityReqDTO);
        }
    }
}
