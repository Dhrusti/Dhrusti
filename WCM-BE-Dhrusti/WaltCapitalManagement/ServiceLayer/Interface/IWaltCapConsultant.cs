using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IWaltCapConsultant
    {
        public CommonResponse GetAllWaltCapConsultant();
        public CommonResponse GetWaltCapConsultantDetailById(GetWaltCapConsultantReqDTO getWaltCapConsultantReqDTO);
        public CommonResponse AddWaltCapConsultant(AddWaltCapConsultantReqDTO addWaltCapConsultantReqDTO);
        public CommonResponse UpdateWaltCapConsultant(UpdateWaltCapConsultantReqDTO addWaltCapConsultantReqDTO);
        public CommonResponse DeleteWaltCapConsultant(DeleteWaltCapConsultantReqDTO deleteWaltCapConsultantReqDTO);

    }
}
