using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class WaltCapConsultantBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        public WaltCapConsultantBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse GetAllWaltCapConsultant()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var waltCapList = _commonRepo.waltCapConsultantList().ToList();
                if (waltCapList.Count > 0)
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
                commonResponse.Data = waltCapList.Adapt<List<GetWaltCapConsultantResDTO>>();
            }
            catch (Exception ex)
            {
                commonResponse.Data = ex.ToString();
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

        public CommonResponse GetWaltCapConsultantDetailById(GetWaltCapConsultantReqDTO getWaltCapConsultantReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetWaltCapConsultantResDTO getWaltCapConsultantResDTO = new GetWaltCapConsultantResDTO();
                getWaltCapConsultantResDTO = _commonRepo.waltCapConsultantList().Where(x => x.Id == getWaltCapConsultantReqDTO.Id).First().Adapt<GetWaltCapConsultantResDTO>();

                if (getWaltCapConsultantResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getWaltCapConsultantResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }

            }
            catch (Exception ex)
            {
                commonResponse.Data = ex.ToString();
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

        public CommonResponse AddWaltCapConsultant(AddWaltCapConsultantReqDTO addWaltCapConsultantReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddWaltCapConsultantResDTO addWaltCapConsultantResDTO = new AddWaltCapConsultantResDTO();
            try
            {
                var walt = _commonRepo.waltCapConsultantList().Where(x => x.WaltCapConsultant.ToLower() == addWaltCapConsultantReqDTO.WaltCapConsultant.ToLower()).ToList();
                if (walt.Count == 0)
                {
                    WaltCapConsultantMst waltCapConsultantMst = new WaltCapConsultantMst();
                    waltCapConsultantMst.WaltCapConsultant = addWaltCapConsultantReqDTO.WaltCapConsultant;
                    waltCapConsultantMst.CreatedBy = addWaltCapConsultantReqDTO.UserId;
                    waltCapConsultantMst.UpdatedBy = addWaltCapConsultantReqDTO.UserId;
                    waltCapConsultantMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    waltCapConsultantMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    waltCapConsultantMst.IsActive = true;
                    waltCapConsultantMst.IsDeleted = false;

                    _dBContext.WaltCapConsultantMsts.Add(waltCapConsultantMst);
                    _dBContext.SaveChanges();

                    addWaltCapConsultantResDTO.Id = waltCapConsultantMst.Id;
                    addWaltCapConsultantResDTO.WaltCapConsultant = waltCapConsultantMst.WaltCapConsultant;

                    commonResponse.Message = "Data added Successfully...!!!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addWaltCapConsultantResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not add Data...!!!";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Data = ex.ToString();
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

        public CommonResponse UpdateWaltCapConsultant(UpdateWaltCapConsultantReqDTO updateWaltCapConsultantReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateWaltCapConsultantResDTO updateWaltCapConsultantResDTO = new UpdateWaltCapConsultantResDTO();
            try
            {
                var waltDetail = _commonRepo.waltCapConsultantList().FirstOrDefault(x => x.Id == updateWaltCapConsultantReqDTO.Id);
                if (waltDetail != null)
                {
                    WaltCapConsultantMst waltCapConsultantMst = waltDetail;
                    waltCapConsultantMst.WaltCapConsultant = updateWaltCapConsultantReqDTO.WaltCapConsultant;
                    waltCapConsultantMst.UpdatedBy = updateWaltCapConsultantReqDTO.UserId;
                    waltCapConsultantMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dBContext.Entry(waltCapConsultantMst).State = EntityState.Modified;
                    _dBContext.SaveChanges();

                    updateWaltCapConsultantResDTO.WaltCapConsultant = waltCapConsultantMst.WaltCapConsultant;

                    commonResponse.Data = updateWaltCapConsultantResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Successfully Updated...!!!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not update the data...!!!";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Data = ex.ToString();
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

        public CommonResponse DeleteWaltCapConsultant(DeleteWaltCapConsultantReqDTO deleteWaltCapConsultantReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteWaltCapConsultantResDTO deleteWaltCapConsultantResDTO = new DeleteWaltCapConsultantResDTO();
            try
            {
                var walt = _commonRepo.waltCapConsultantList().FirstOrDefault(x => x.Id == deleteWaltCapConsultantReqDTO.Id);
                if (walt != null)
                {
                    WaltCapConsultantMst waltCapConsultantMst = walt;
                    waltCapConsultantMst.Id = deleteWaltCapConsultantReqDTO.Id;
                    waltCapConsultantMst.UpdatedBy = deleteWaltCapConsultantReqDTO.UserId;
                    waltCapConsultantMst.IsDeleted = true;
                    waltCapConsultantMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dBContext.Entry(waltCapConsultantMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dBContext.SaveChanges();

                    deleteWaltCapConsultantResDTO.Id = waltCapConsultantMst.Id;

                    commonResponse.Data = deleteWaltCapConsultantResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Deleted Successfully...!!!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not delete the data...!!!";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Data = ex.ToString();
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }
    }
}
