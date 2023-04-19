using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class RegionBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;

        public RegionBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
        }

        public CommonResponse GetAllCountry()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var countryList = _dbContext.CountryMsts.ToList();
                if (countryList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                }
                commonResponse.Data = countryList.Adapt<List<GetCountryResDTO>>();
            }
            catch (Exception)
            {

                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllStateByCountryID(GetStateReqDTO getStateReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetStateResDTO> getStateResDTO = new List<GetStateResDTO>();
                getStateResDTO = _dbContext.StateMsts.Where(x => x.CountryId == getStateReqDTO.CountryId).ToList().Adapt<List<GetStateResDTO>>();

                if (getStateResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getStateResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllCityByStateID(GetCityReqDTO getCityReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetCityResDTO> getCityResDTO = new List<GetCityResDTO>();
                getCityResDTO = _dbContext.CityMsts.Where(x => x.StateId == getCityReqDTO.StateId).ToList().Adapt<List<GetCityResDTO>>();

                if (getCityResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getCityResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllCountryCustom()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                CountryMst countryMst = new CountryMst();
                List<GetAllCountryCustomResDTO> countryList = (from c in _dbContext.CountryMsts
                                                               join
                                                              d in _dbContext.CountryCustomMsts
                                                              on c.CountryName.ToLower() equals d.CountryName
                                                               select new { c, d }).Select(x => new GetAllCountryCustomResDTO
                                                               {
                                                                   CountryId = x.d.CountryId,
                                                                   CountryName = x.d.CountryName,
                                                                   Iso2 = x.c.Iso2,
                                                                   PhoneCode = x.c.PhoneCode
                                                               }).OrderBy(x => x.CountryId).ToList();

                if (countryList != null)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                }
                commonResponse.Data = countryList.Adapt<List<GetAllCountryCustomResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllStateCustomByCountryId(GetAllStateCustomReqDTO getAllStateCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                //var country= _commonRepo.countryCustomList().Where(x => x.CountryId == getAllStateCustomReqDTO.CountryId).FirstOrDefault();
                var countryCustomMsts = (from c in _dbContext.CountryCustomMsts.Where(x => x.CountryId == getAllStateCustomReqDTO.CountryId)
                                         join
                 d in _dbContext.CountryMsts
                 on c.CountryName.ToLower() equals d.CountryName
                                         select new { c, d }).Select(x => new GetAllStateCustomResDTO
                                         {
                                             CountryId = x.c.CountryId,
                                             CountryName = x.c.CountryName,
                                             Iso2 = x.d.Iso2,
                                             PhoneCode = x.d.PhoneCode
                                         }).OrderBy(x => x.CountryId).FirstOrDefault();

                List<StateList> stateList = _commonRepo.stateCustomList().Where(x => x.CountryId == countryCustomMsts.CountryId).ToList().Select(x => new StateList
                {
                    StateId = x.StateId,
                    StateName = x.StateName
                }).OrderBy(x => x.StateId).ToList();

                GetAllStateCustomResDTO getStateResDTO = new();
                getStateResDTO.CountryId = countryCustomMsts.CountryId;
                getStateResDTO.CountryName = countryCustomMsts.CountryName;
                getStateResDTO.Iso2 = countryCustomMsts.Iso2;
                getStateResDTO.PhoneCode = countryCustomMsts.PhoneCode;
                getStateResDTO.stateList = stateList;

                if (getStateResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getStateResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllCityCustomByStateId(GetAllCityCustomReqDTO getAllCityCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var statcustomMst = _commonRepo.stateCustomList().Where(x => x.StateId == getAllCityCustomReqDTO.StateId).FirstOrDefault();

                List<CityList> cityList = _commonRepo.cityCustomList().Where(x => x.StateId == statcustomMst.StateId).ToList().Select(x => new CityList
                {
                    CityId = x.CityId,
                    CityName = x.CityName,
                }).OrderBy(x => x.CityId).ToList();

                GetAllCityCustomResDTO getCityResDTO = new();
                getCityResDTO.stateId = statcustomMst.StateId;
                getCityResDTO.StateName = statcustomMst.StateName;
                getCityResDTO.cityList = cityList;

                if (getCityResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getCityResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddCountryCustom(AddCountryCustomReqDTO addCountryCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddCountryCustomResDTO addCountryCustomResDTO = new AddCountryCustomResDTO();
            try
            {

                var countyMst = _dbContext.CountryMsts.Where(x => x.CountryName.ToLower() == addCountryCustomReqDTO.CountryName.ToLower() || x.CountryName.ToLower().Replace(" ", string.Empty) == addCountryCustomReqDTO.CountryName.ToLower()).FirstOrDefault();

                var countyMst2 = _dbContext.CountryMsts.Where(x => x.CountryName.ToLower() == addCountryCustomReqDTO.CountryName.ToLower() || x.CountryName.ToLower().Replace(" ", string.Empty) == addCountryCustomReqDTO.CountryName.ToLower()).FirstOrDefault();

                var countryCustomMsts = _commonRepo.countryCustomList().Where(x => x.CountryName.ToLower() == addCountryCustomReqDTO.CountryName.ToLower() || x.CountryName.ToLower().Replace(" ", string.Empty) == addCountryCustomReqDTO.CountryName.ToLower()).FirstOrDefault();
                if (countyMst != null)
                {
                    if (countryCustomMsts == null)
                    {
                        CountryCustomMst countryCustomMst = new CountryCustomMst();
                        countryCustomMst.CountryName = addCountryCustomReqDTO.CountryName[0].ToString().ToUpper() + addCountryCustomReqDTO.CountryName.Substring(1);
                        countryCustomMst.CreatedBy = addCountryCustomReqDTO.CreatedBy;

                        _dbContext.CountryCustomMsts.Add(countryCustomMst);
                        _dbContext.SaveChanges();

                        addCountryCustomResDTO.CountryId = countryCustomMst.CountryId;
                        addCountryCustomResDTO.CountryName = countryCustomMst.CountryName;

                        commonResponse.Message = "Country added successfully";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = addCountryCustomResDTO;

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Country is already exist!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Enter Valid Country Name!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddStateCustom(AddStateCustomReqDTO addStateCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddStateCustomResDTO addStateCustomResDTO = new AddStateCustomResDTO();
            try
            {
                var stateMst = _dbContext.StateMsts.Where(x => x.StateName.ToLower() == addStateCustomReqDTO.StateName.ToLower() || x.StateName.ToLower().Replace(" ", string.Empty) == addStateCustomReqDTO.StateName.ToLower()).FirstOrDefault();

                var stateCustomMsts = _commonRepo.stateCustomList().Where(x => (x.StateName.ToLower() == addStateCustomReqDTO.StateName.ToLower() || x.StateName.ToLower().Replace(" ", string.Empty) == addStateCustomReqDTO.StateName.ToLower()) && x.CountryId == addStateCustomReqDTO.CountryId).FirstOrDefault();

                if (stateMst != null)
                {
                    if (stateCustomMsts == null)
                    {
                        StateCustomMst stateCustomMst = new StateCustomMst();
                        stateCustomMst.StateName = addStateCustomReqDTO.StateName[0].ToString().ToUpper() + addStateCustomReqDTO.StateName.Substring(1);
                        stateCustomMst.CountryId = addStateCustomReqDTO.CountryId;
                        stateCustomMst.CreatedBy = addStateCustomReqDTO.CreatedBy;


                        _dbContext.StateCustomMsts.Add(stateCustomMst);
                        _dbContext.SaveChanges();

                        addStateCustomResDTO.StateId = stateCustomMst.StateId;
                        addStateCustomResDTO.StateName = stateCustomMst.StateName;

                        commonResponse.Message = "Province added successfully";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = addStateCustomResDTO;
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Province is already exist!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Enter Valid Province Name!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddCityCustom(AddCityCustomReqDTO addCityCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddCityCustomResDTO addCityCustomResDTO = new AddCityCustomResDTO();
            try
            {
                var cityCustom = _commonRepo.cityCustomList().Where(x => x.CityName.ToLower() == addCityCustomReqDTO.CityName.ToLower()).ToList();


                if (cityCustom.Count == 0)
                {
                    CityCustomMst cityCustomMst = new CityCustomMst();
                    cityCustomMst.CityName = addCityCustomReqDTO.CityName;
                    cityCustomMst.StateId = addCityCustomReqDTO.StateId;
                    cityCustomMst.CreatedBy = addCityCustomReqDTO.CreatedBy;


                    _dbContext.CityCustomMsts.Add(cityCustomMst);
                    _dbContext.SaveChanges();

                    addCityCustomResDTO.CityId = cityCustomMst.CityId;
                    addCityCustomResDTO.CityName = cityCustomMst.CityName;

                    commonResponse.Message = "City added successfully";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addCityCustomResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "City is already exist!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateCountryCustom(UpdateCountryCustomReqDTO updateCountryCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateCountryCustomResDTO updateCountryCustomResDTO = new UpdateCountryCustomResDTO();
            try
            {
                var countryCustomMst = _commonRepo.countryCustomList().FirstOrDefault(x => x.CountryId == updateCountryCustomReqDTO.CountryId);
                if (countryCustomMst != null)
                {
                    var countryNameCustom = _commonRepo.countryCustomList().FirstOrDefault(x => x.CountryName == updateCountryCustomReqDTO.CountryName);

                    if (countryCustomMst.CountryName.ToLower() == updateCountryCustomReqDTO.CountryName.ToLower())
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Country Name Already Updated!";
                    }
                    else if (countryNameCustom == null)
                    {
                        var countyMst = _dbContext.CountryMsts.Where(x => x.CountryName.ToLower() == updateCountryCustomReqDTO.CountryName.ToLower()).FirstOrDefault();

                        if (countyMst != null)
                        {
                            countryCustomMst.CountryName = updateCountryCustomReqDTO.CountryName[0].ToString().ToUpper() + updateCountryCustomReqDTO.CountryName.Substring(1);
                            countryCustomMst.CreatedBy = updateCountryCustomReqDTO.UpdatedBy;

                            _dbContext.Entry(countryCustomMst).State = EntityState.Modified;
                            _dbContext.SaveChanges();

                            updateCountryCustomResDTO.CountryId = countryCustomMst.CountryId;

                            commonResponse.Data = updateCountryCustomResDTO;
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Successfully Updated";

                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Enter Valid Country Name!";
                        }
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Country is already exist!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not find data!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteCountryCustom(DeleteCountryCustomReqDTO deleteCountryCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteCountryCustomResDTO deleteCountryCustomResDTO = new DeleteCountryCustomResDTO();
            try
            {
                var deleteCountryCustom = _commonRepo.countryCustomList().FirstOrDefault(x => x.CountryId == deleteCountryCustomReqDTO.Id);
                if (deleteCountryCustom != null)
                {
                    var stateisexist = _commonRepo.stateCustomList().Where(x => x.CountryId == deleteCountryCustomReqDTO.Id).FirstOrDefault();
                    if (stateisexist == null)
                    {
                        _dbContext.CountryCustomMsts.Remove(deleteCountryCustom);
                        _dbContext.SaveChanges();

                        deleteCountryCustomResDTO.Id = deleteCountryCustom.CountryId;

                        commonResponse.Data = deleteCountryCustomResDTO;
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Deleted Successfully!";

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "This country already have Province!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not find data.!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateStateCustom(UpdateStateCustomReqDTO updateStateCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateStateCustomResDTO updateStateCustomResDTO = new UpdateStateCustomResDTO();
            try
            {
                var stateCustomMst = _commonRepo.stateCustomList().FirstOrDefault(x => x.StateId == updateStateCustomReqDTO.StateId);
                if (stateCustomMst != null)
                {
                    var stateNameCustom = _commonRepo.stateCustomList().FirstOrDefault(x => x.StateName == updateStateCustomReqDTO.StateName);
                    if (stateCustomMst.StateName.ToLower() == updateStateCustomReqDTO.StateName.ToLower())
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Province Name Already Updated!";
                    }
                    else if (stateNameCustom == null)
                    {
                        var stateMst = _dbContext.StateMsts.Where(x => x.StateName.ToLower() == updateStateCustomReqDTO.StateName.ToLower()).FirstOrDefault();
                        if (stateMst != null)
                        {
                            stateCustomMst.StateName = updateStateCustomReqDTO.StateName[0].ToString().ToUpper() + updateStateCustomReqDTO.StateName.Substring(1);
                            stateCustomMst.CountryId = updateStateCustomReqDTO.CountryId;
                            stateCustomMst.CreatedBy = updateStateCustomReqDTO.UpdatedBy;

                            _dbContext.Entry(stateCustomMst).State = EntityState.Modified;
                            _dbContext.SaveChanges();

                            updateStateCustomResDTO.StateId = stateCustomMst.StateId;

                            commonResponse.Data = updateStateCustomResDTO;
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Successfully Updated";
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Enter Valid Province Name!";
                        }
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Province is already exist!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not find data!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteStateCustom(DeleteStateCustomReqDTO deleteStateCustomReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteStateCustomResDTO deleteStateCustomResDTO = new DeleteStateCustomResDTO();
            try
            {
                var deleteStateCustom = _commonRepo.stateCustomList().FirstOrDefault(x => x.StateId == deleteStateCustomReqDTO.Id);
                if (deleteStateCustom != null)
                {
                    var cityisexist = _commonRepo.cityCustomList().Where(x => x.StateId == deleteStateCustomReqDTO.Id).FirstOrDefault();
                    if (cityisexist == null)
                    {

                        _dbContext.StateCustomMsts.Remove(deleteStateCustom);
                        _dbContext.SaveChanges();

                        deleteStateCustomResDTO.Id = deleteStateCustom.StateId;

                        commonResponse.Data = deleteStateCustomResDTO;
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Deleted Successfully";

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "This Province already have city!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not find the data";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetCountryByCountryId(GetCountryByCountryIdReqDTO getByCountryIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetCountryByCountryIdResDTO> getByCountryIdResDTO = new List<GetCountryByCountryIdResDTO>();
                getByCountryIdResDTO = _commonRepo.countryCustomList().Where(x => x.CountryId == getByCountryIdReqDTO.CountryId).ToList().Adapt<List<GetCountryByCountryIdResDTO>>();

                if (getByCountryIdResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getByCountryIdResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetStateByStateId(GetStateByStateIdReqDTO getByStateIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetStateByStateIdResDTO> getByStateIdResDTO = new List<GetStateByStateIdResDTO>();
                getByStateIdResDTO = _commonRepo.stateCustomList().Where(x => x.StateId == getByStateIdReqDTO.StateId).ToList().Adapt<List<GetStateByStateIdResDTO>>();

                if (getByStateIdResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getByStateIdResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetCityByCityId(GetCityByCityIdReqDTO getByCityIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetCityByCityIdResDTO> getByCityIdResDTO = new List<GetCityByCityIdResDTO>();
                getByCityIdResDTO = _commonRepo.cityCustomList().Where(x => x.CityId == getByCityIdReqDTO.CityId).ToList().Adapt<List<GetCityByCityIdResDTO>>();

                if (getByCityIdResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getByCityIdResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddCustomCity(AddCustomCityReqDTO addCustomCityReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddCustomCityResDTO addCustomCityResDTO = new AddCustomCityResDTO();
            try
            {

                var customCity = _commonRepo.cityCustomList().Where(x => (x.CityName.ToLower() == addCustomCityReqDTO.CityName.ToLower() || x.CityName.ToLower().Replace(" ", string.Empty) == addCustomCityReqDTO.CityName.ToLower()) && x.StateId == addCustomCityReqDTO.StateId).FirstOrDefault();

                var City = _dbContext.CityMsts.Where(x => x.CityName.ToLower() == addCustomCityReqDTO.CityName.ToLower()).FirstOrDefault();

                if (City != null)
                {
                    if (customCity == null)
                    {
                        CityCustomMst cityCustomMst = new CityCustomMst();
                        cityCustomMst.CityName = addCustomCityReqDTO.CityName[0].ToString().ToUpper() + addCustomCityReqDTO.CityName.Substring(1); ;
                        cityCustomMst.CreatedBy = addCustomCityReqDTO.CreatedBy;
                        cityCustomMst.StateId = addCustomCityReqDTO.StateId;

                        _dbContext.CityCustomMsts.Add(cityCustomMst);
                        _dbContext.SaveChanges();

                        addCustomCityResDTO.CityId = cityCustomMst.CityId;
                        addCustomCityResDTO.CityName = cityCustomMst.CityName;

                        commonResponse.Message = "City Added Successfully!";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = addCustomCityResDTO;
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "City Name Already Exist!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Enter Valid City Name!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateCustomCity(UpdateCustomCityReqDTO updateCustomCityReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateCustomCityResDTO updateCustomCityResDTO = new UpdateCustomCityResDTO();
            try
            {
                var customCityDetail = _commonRepo.cityCustomList().FirstOrDefault(x => x.CityId == updateCustomCityReqDTO.CityId);

                if (customCityDetail.CityName.ToLower() == updateCustomCityReqDTO.CityName.ToLower())
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "City Name Already Updated!";
                }
                else if (customCityDetail != null)
                {
                    CityCustomMst cityCustomMst = customCityDetail;
                    cityCustomMst.CityName = updateCustomCityReqDTO.CityName[0].ToString().ToUpper() + updateCustomCityReqDTO.CityName.Substring(1);
                    cityCustomMst.CreatedBy = updateCustomCityReqDTO.UpdatedBy;
                    cityCustomMst.StateId = updateCustomCityReqDTO.StateId;

                    _dbContext.Entry(cityCustomMst).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    updateCustomCityResDTO.CityName = cityCustomMst.CityName;

                    commonResponse.Data = updateCustomCityResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Successfully Updated!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not update the data!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteCustomCity(DeleteCustomCityReqDTO deleteCustomCityReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteCustomCityResDTO deleteCustomCityResDTO = new DeleteCustomCityResDTO();
            try
            {
                var customCity = _commonRepo.cityCustomList().FirstOrDefault(x => x.CityId == deleteCustomCityReqDTO.CityId);
                if (customCity != null)
                {
                    var officeisexist = _commonRepo.officeList().Where(x => x.CityId == deleteCustomCityReqDTO.CityId).FirstOrDefault();
                    if (officeisexist == null)
                    {

                        CityCustomMst cityCustomMst = customCity;
                        cityCustomMst.CityId = deleteCustomCityReqDTO.CityId;


                        _dbContext.Entry(cityCustomMst).State = EntityState.Deleted;
                        _dbContext.SaveChanges();

                        deleteCustomCityResDTO.CityId = cityCustomMst.CityId;

                        commonResponse.Data = deleteCustomCityResDTO;
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Deleted Successfully";

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "This city already have office!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not find the data!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
