using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class ClientTypeBLL
    {

        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public ClientTypeBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse GetAllClientType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var clientTypeList = _commonRepo.clientTypeList().ToList();
                if (clientTypeList.Count > 0)
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
                commonResponse.Data = clientTypeList.Adapt<List<GetAllClientTypeResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetByClientTypeId(GetClientTypeByIdReqDTO getClientTypeByIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetClientTypeByIdResDTO getClientTypeByIdResDTO = new GetClientTypeByIdResDTO();

                getClientTypeByIdResDTO = _commonRepo.clientTypeList().Where(x => x.Id == getClientTypeByIdReqDTO.Id).FirstOrDefault().Adapt<GetClientTypeByIdResDTO>();

                if (getClientTypeByIdResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getClientTypeByIdResDTO;
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

        public CommonResponse AddClientType(AddClientTypeReqDTO addClientTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddClientTypeResDTO addClientTypeResDTO = new AddClientTypeResDTO();
            try
            {
                var clientType = _commonRepo.clientTypeList().Where(x => x.ClientType.ToLower() == addClientTypeReqDTO.ClientType.ToLower()).ToList();
                if (clientType.Count == 0)
                {
                    ClientTypeMst clientTypeMst = new ClientTypeMst();
                    clientTypeMst.ClientType = addClientTypeReqDTO.ClientType;
                    clientTypeMst.CreatedBy = addClientTypeReqDTO.CreatedBy;
                    clientTypeMst.UpdatedBy = addClientTypeReqDTO.CreatedBy;
                    clientTypeMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    clientTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    clientTypeMst.IsActive = true;
                    clientTypeMst.IsDeleted = false;

                    _dbContext.ClientTypeMsts.Add(clientTypeMst);
                    _dbContext.SaveChanges();

                    addClientTypeResDTO.Id = clientTypeMst.Id;
                    addClientTypeResDTO.ClientType = clientTypeMst.ClientType;

                    commonResponse.Message = "ClientType added successfully!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addClientTypeResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not add clientType!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateClientType(UpdateClientTypeReqDTO updateClientTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateClientTypeResDTO clientTypeResDTO = new UpdateClientTypeResDTO();
            try
            {
                var clientTypeDetail = _commonRepo.clientTypeList().FirstOrDefault(x => x.Id == updateClientTypeReqDTO.Id);
                if (clientTypeDetail != null)
                {
                    clientTypeDetail.ClientType = updateClientTypeReqDTO.ClientType;
                    clientTypeDetail.UpdatedBy = updateClientTypeReqDTO.UpdatedBy;
                    clientTypeDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(clientTypeDetail).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    clientTypeResDTO.ClientType = clientTypeDetail.ClientType;
                    clientTypeResDTO.Id = clientTypeDetail.Id;

                    commonResponse.Data = clientTypeResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Updated Successfully!";
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

        public CommonResponse DeleteClientType(DeleteClientTypeReqDTO deleteClientTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteClientTypeResDTO deleteClientTypeResDTO = new DeleteClientTypeResDTO();
            try
            {
                var clientType = _commonRepo.clientTypeList().FirstOrDefault(x => x.Id == deleteClientTypeReqDTO.Id);
                if (clientType != null)
                {

                    clientType.Id = deleteClientTypeReqDTO.Id;
                    clientType.UpdatedBy = deleteClientTypeReqDTO.UpdatedBy;
                    clientType.IsDeleted = true;
                    clientType.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(clientType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dbContext.SaveChanges();

                    deleteClientTypeResDTO.Id = clientType.Id;

                    commonResponse.Data = deleteClientTypeResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Deleted Successfully!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not delete the data!";
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
