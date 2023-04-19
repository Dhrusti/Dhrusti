using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class WaltCapConsultantImpl : IWaltCapConsultant
    {
        private readonly WaltCapConsultantBLL _iwaltCapConsultantBLL;
        public WaltCapConsultantImpl(WaltCapConsultantBLL iwaltCapConsultantBLL)
        {
            _iwaltCapConsultantBLL = iwaltCapConsultantBLL;
        }

        public CommonResponse GetAllWaltCapConsultant()
        {
            return _iwaltCapConsultantBLL.GetAllWaltCapConsultant();
        }

        public CommonResponse GetWaltCapConsultantDetailById(GetWaltCapConsultantReqDTO getWaltCapConsultantReqDTO)
        {
            return _iwaltCapConsultantBLL.GetWaltCapConsultantDetailById(getWaltCapConsultantReqDTO);
        }

        public CommonResponse AddWaltCapConsultant(AddWaltCapConsultantReqDTO addWaltCapConsultantReqDTO)
        {
            return _iwaltCapConsultantBLL.AddWaltCapConsultant(addWaltCapConsultantReqDTO);
        }

        public CommonResponse UpdateWaltCapConsultant(UpdateWaltCapConsultantReqDTO addWaltCapConsultantReqDTO)
        {
            return _iwaltCapConsultantBLL.UpdateWaltCapConsultant(addWaltCapConsultantReqDTO);
        }

        public CommonResponse DeleteWaltCapConsultant(DeleteWaltCapConsultantReqDTO deleteWaltCapConsultantReqDTO)
        {
            return _iwaltCapConsultantBLL.DeleteWaltCapConsultant(deleteWaltCapConsultantReqDTO);
        }


    }
}
