using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class ServiceProviderBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public ServiceProviderBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse GetAllServiceProvider()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var serviceProviderList = _commonRepo.serviceProviderList().ToList();
                if (serviceProviderList.Count > 0)
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
                commonResponse.Data = serviceProviderList.Adapt<List<GetAllServiceProviderResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetByServiceProviderId(GetServiceProviderByIdReqDTO getServiceProviderByIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetServiceProviderByIdResDTO getserviceProviderByIdResDTO = new GetServiceProviderByIdResDTO();
                getserviceProviderByIdResDTO = _commonRepo.serviceProviderList().Where(x => x.Id == getServiceProviderByIdReqDTO.Id).FirstOrDefault().Adapt<GetServiceProviderByIdResDTO>();

                if (getserviceProviderByIdResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getserviceProviderByIdResDTO;
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

        public CommonResponse AddServiceProvider(AddServiceProviderReqDTO addServiceProviderReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddServiceProviderResDTO addServiceProviderResDTO = new AddServiceProviderResDTO();
            try
            {
                var serviceProvider = _commonRepo.serviceProviderList().Where(x => x.ServiceProvider.ToLower() == addServiceProviderReqDTO.ServiceProvider.ToLower()).ToList();
                if (serviceProvider.Count == 0)
                {
                    ServiceProviderMst serviceProviderMst = new ServiceProviderMst();
                    serviceProviderMst.ServiceProvider = addServiceProviderReqDTO.ServiceProvider;
                    serviceProviderMst.CreatedBy = addServiceProviderReqDTO.CreatedBy;
                    serviceProviderMst.UpdatedBy = addServiceProviderReqDTO.CreatedBy;
                    serviceProviderMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    serviceProviderMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    serviceProviderMst.IsActive = true;
                    serviceProviderMst.IsDeleted = false;

                    _dbContext.ServiceProviderMsts.Add(serviceProviderMst);
                    _dbContext.SaveChanges();

                    addServiceProviderResDTO.Id = serviceProviderMst.Id;
                    addServiceProviderResDTO.ServiceProvider = serviceProviderMst.ServiceProvider;

                    commonResponse.Message = "ServiceProvider added Successfully";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addServiceProviderResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not add ServiceProvider";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateServiceProvider(UpdateServiceProviderReqDTO updateServiceProviderReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateServiceProviderResDTO updateServiceProviderResDTO = new UpdateServiceProviderResDTO();
            try
            {
                var ServiceProviderDetail = _commonRepo.serviceProviderList().FirstOrDefault(x => x.Id == updateServiceProviderReqDTO.Id);
                if (ServiceProviderDetail != null)
                {
                    ServiceProviderDetail.ServiceProvider = updateServiceProviderReqDTO.ServiceProvider;
                    ServiceProviderDetail.UpdatedBy = updateServiceProviderReqDTO.UpdatedBy;
                    ServiceProviderDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(ServiceProviderDetail).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    updateServiceProviderResDTO.ServiceProvider = ServiceProviderDetail.ServiceProvider;
                    updateServiceProviderResDTO.Id = ServiceProviderDetail.Id;

                    commonResponse.Data = updateServiceProviderResDTO;
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

        public CommonResponse DeleteServiceProvider(DeleteServiceProviderReqDTO deleteServiceProviderReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteServiceProviderResDTO deleteServiceProviderResDTO = new DeleteServiceProviderResDTO();
            try
            {
                var serviceProvider = _commonRepo.serviceProviderList().FirstOrDefault(x => x.Id == deleteServiceProviderReqDTO.Id);
                if (serviceProvider != null)
                {

                    serviceProvider.Id = deleteServiceProviderReqDTO.Id;
                    serviceProvider.UpdatedBy = deleteServiceProviderReqDTO.UpdatedBy;
                    serviceProvider.IsDeleted = true;
                    serviceProvider.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(serviceProvider).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dbContext.SaveChanges();

                    deleteServiceProviderResDTO.Id = serviceProvider.Id;

                    commonResponse.Data = deleteServiceProviderResDTO;
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
