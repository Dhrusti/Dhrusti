using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class ServiceProviderTypeBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public ServiceProviderTypeBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse GetAllServiceProviderType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var serviceProviderTypeList = _commonRepo.serviceProviderTypeList().ToList();
                if (serviceProviderTypeList.Count > 0)
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
                commonResponse.Data = serviceProviderTypeList.Adapt<List<GetAllServiceProviderTypeResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetByServiceProviderTypeId(GetServiceProviderTypeByIdReqDTO getServiceProviderTypeByIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetServiceProviderTypeByIdResDTO getserviceProviderTypeByIdResDTO = new GetServiceProviderTypeByIdResDTO();
                getserviceProviderTypeByIdResDTO = _commonRepo.serviceProviderTypeList().Where(x => x.Id == getServiceProviderTypeByIdReqDTO.Id).FirstOrDefault().Adapt<GetServiceProviderTypeByIdResDTO>();

                if (getserviceProviderTypeByIdResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getserviceProviderTypeByIdResDTO;
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

        public CommonResponse AddServiceProviderType(AddServiceProviderTypeReqDTO addServiceProviderTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddServiceProviderTypeResDTO addServiceProviderTypeResDTO = new AddServiceProviderTypeResDTO();
            try
            {
                var serviceProviderType = _commonRepo.serviceProviderTypeList().Where(x => x.ServiceProviderType.ToLower() == addServiceProviderTypeReqDTO.ServiceProviderType.ToLower()).ToList();
                if (serviceProviderType.Count == 0)
                {
                    ServiceProviderTypeMst serviceProviderTypeMst = new ServiceProviderTypeMst();
                    serviceProviderTypeMst.ServiceProviderType = addServiceProviderTypeReqDTO.ServiceProviderType;
                    serviceProviderTypeMst.CreatedBy = addServiceProviderTypeReqDTO.CreatedBy;
                    serviceProviderTypeMst.UpdatedBy = addServiceProviderTypeReqDTO.CreatedBy;
                    serviceProviderTypeMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    serviceProviderTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    serviceProviderTypeMst.IsActive = true;
                    serviceProviderTypeMst.IsDeleted = false;

                    _dbContext.ServiceProviderTypeMsts.Add(serviceProviderTypeMst);
                    _dbContext.SaveChanges();

                    addServiceProviderTypeResDTO.Id = serviceProviderTypeMst.Id;
                    addServiceProviderTypeResDTO.ServiceProviderType = serviceProviderTypeMst.ServiceProviderType;

                    commonResponse.Message = "ServiceProviderType added successfully";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addServiceProviderTypeResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not add serviceProviderType";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateServiceProviderType(UpdateServiceProviderTypeReqDTO updateServiceProviderTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateServiceProviderTypeResDTO updateServiceProviderTypeResDTO = new UpdateServiceProviderTypeResDTO();
            try
            {
                var ServiceProviderTypeDetail = _commonRepo.serviceProviderTypeList().FirstOrDefault(x => x.Id == updateServiceProviderTypeReqDTO.Id);
                if (ServiceProviderTypeDetail != null)
                {
                    ServiceProviderTypeDetail.ServiceProviderType = updateServiceProviderTypeReqDTO.ServiceProviderType;
                    ServiceProviderTypeDetail.UpdatedBy = updateServiceProviderTypeReqDTO.UpdateBy;
                    ServiceProviderTypeDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(ServiceProviderTypeDetail).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    updateServiceProviderTypeResDTO.ServiceProviderType = ServiceProviderTypeDetail.ServiceProviderType;
                    updateServiceProviderTypeResDTO.Id = ServiceProviderTypeDetail.Id;

                    commonResponse.Data = updateServiceProviderTypeResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Successfully Updated";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not update the data";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteServiceProviderType(DeleteServiceProviderTypeReqDTO deleteServiceProviderTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteServiceProviderTypeResDTO deleteServiceProviderTypeResDTO = new DeleteServiceProviderTypeResDTO();
            try
            {
                var serviceProviderType = _commonRepo.serviceProviderTypeList().FirstOrDefault(x => x.Id == deleteServiceProviderTypeReqDTO.Id);
                if (serviceProviderType != null)
                {

                    serviceProviderType.Id = deleteServiceProviderTypeReqDTO.Id;
                    serviceProviderType.UpdatedBy = deleteServiceProviderTypeReqDTO.UpdatedBy;
                    serviceProviderType.IsDeleted = true;
                    serviceProviderType.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(serviceProviderType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dbContext.SaveChanges();

                    deleteServiceProviderTypeResDTO.Id = serviceProviderType.Id;

                    commonResponse.Data = deleteServiceProviderTypeResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Deleted Successfully";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not delete the data";
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
