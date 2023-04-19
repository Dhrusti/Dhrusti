using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IOffice
    {
        public CommonResponse GetAllOffice();
        public CommonResponse GetOfficeByCityId(GetOfficeByCityIdReqDTO getOfficeByCityIdReqDTO);
        public CommonResponse GetOfficeDetailById(GetOfficeReqDTO getOfficeReqDTO);
        public CommonResponse AddOffice(AddOfficeReqDTO addOfficeReqDTO);
        public CommonResponse UpdateOffice(UpdateOfficeReqDTO updateOfficeReqDTO);
        public CommonResponse DeleteOffice(DeleteOfficeReqDTO deleteOfficeReqDTO);
    }
}
