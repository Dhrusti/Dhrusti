using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class OfficeBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public OfficeBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse GetAllOffice()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var officeList = _commonRepo.officeList().ToList();
                if (officeList.Count > 0)
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
                commonResponse.Data = officeList.Adapt<List<GetOfficeResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetOfficeByCityId(GetOfficeByCityIdReqDTO getOfficeByCityIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetOffice> getOffices = _commonRepo.officeList().Where(x => x.CityId == getOfficeByCityIdReqDTO.CityId).ToList().Select(x => new GetOffice
                {
                    officeId = x.Id,
                    officeName = x.Office
                }).ToList();
                var officeCityDetails = _commonRepo.cityCustomList().Where(x => x.CityId == getOfficeByCityIdReqDTO.CityId).FirstOrDefault();
                if (officeCityDetails != null)
                {

                    GetOfficeByCityIdResDTO getOfficeByCityIdResDTO = new GetOfficeByCityIdResDTO();
                    getOfficeByCityIdResDTO.CityId = officeCityDetails.CityId;
                    getOfficeByCityIdResDTO.CityName = officeCityDetails.CityName;
                    getOfficeByCityIdResDTO.offices = getOffices;

                    if (getOfficeByCityIdResDTO != null)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Success.";
                        commonResponse.Data = getOfficeByCityIdResDTO.Adapt<GetOfficeByCityIdResDTO>();
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data not Found.";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetOfficeDetailById(GetOfficeReqDTO getOfficeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetOfficeResDTO getOfficeResDTO = new GetOfficeResDTO();
                getOfficeResDTO = _commonRepo.officeList().Where(x => x.Id == getOfficeReqDTO.Id).FirstOrDefault().Adapt<GetOfficeResDTO>();

                if (getOfficeResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getOfficeResDTO;
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

        public CommonResponse AddOffice(AddOfficeReqDTO addOfficeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddOfficeResDTO addOfficeResDTO = new AddOfficeResDTO();
            try
            {
                var office = _commonRepo.officeList().Where(x => x.Office.ToLower() == addOfficeReqDTO.Office.ToLower()).FirstOrDefault();
                if (office == null)
                {
                    OfficeMst officeMst = new OfficeMst();
                    officeMst.Office = addOfficeReqDTO.Office;
                    officeMst.CityId = addOfficeReqDTO.CityId;
                    officeMst.CreatedBy = addOfficeReqDTO.UserId;
                    officeMst.UpdatedBy = addOfficeReqDTO.UserId;
                    officeMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    officeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    officeMst.IsActive = true;
                    officeMst.IsDeleted = false;

                    _dbContext.OfficeMsts.Add(officeMst);
                    _dbContext.SaveChanges();

                    addOfficeResDTO.Id = officeMst.Id;
                    addOfficeResDTO.Office = officeMst.Office;

                    commonResponse.Message = "Office added Successfully!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addOfficeResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Office Name Already Exist!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateOffice(UpdateOfficeReqDTO updateOfficeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateOfficeResDTO updateOfficeResDTO = new UpdateOfficeResDTO();
            try
            {

                var office = _commonRepo.officeList().Where(x => x.Id != updateOfficeReqDTO.Id && (x.Office.ToLower() == updateOfficeReqDTO.Office.ToLower())).ToList();
                if (office.Count == 0)
                {
                    var officeDetail = _commonRepo.officeList().FirstOrDefault(x => x.Id == updateOfficeReqDTO.Id);
                    if (officeDetail != null)
                    {
                        OfficeMst officeMst = officeDetail;
                        officeMst.Office = updateOfficeReqDTO.Office;
                        officeMst.CityId = updateOfficeReqDTO.CityId;
                        officeMst.UpdatedBy = updateOfficeReqDTO.UserId;
                        officeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dbContext.Entry(officeMst).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        updateOfficeResDTO.Office = officeMst.Office;

                        commonResponse.Data = updateOfficeResDTO;
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Successfully Updated!";
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Can not find the office!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Office Name Already Exist!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteOffice(DeleteOfficeReqDTO deleteOfficeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteOfficeResDTO deleteOfficeResDTO = new DeleteOfficeResDTO();
            try
            {
                var office = _commonRepo.officeList().FirstOrDefault(x => x.Id == deleteOfficeReqDTO.Id);
                if (office != null)
                {
                    var isexistclient = _commonRepo.ALLUserList().Where(x => x.Office == office.Id && x.IsDeleted == false).ToList();
                    if (isexistclient.Count == 0)
                    {

                        OfficeMst officeMst = office;
                        officeMst.Id = deleteOfficeReqDTO.Id;
                        officeMst.UpdatedBy = deleteOfficeReqDTO.UserId;
                        officeMst.IsDeleted = true;
                        officeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dbContext.Entry(officeMst).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        deleteOfficeResDTO.Id = officeMst.Id;

                        commonResponse.Data = deleteOfficeResDTO;
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Deleted Successfully...!!!";

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "This Office Is Having Clients, So Can Not Delete!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not find the data.!";
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
