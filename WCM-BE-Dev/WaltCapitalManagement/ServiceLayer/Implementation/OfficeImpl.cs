using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class OfficeImpl : IOffice
    {
        private readonly OfficeBLL _officeBLL;

        public OfficeImpl(OfficeBLL officeBLL)
        {
            _officeBLL = officeBLL;
        }

        public CommonResponse GetAllOffice()
        {
            return _officeBLL.GetAllOffice();
        }
        public CommonResponse GetOfficeByCityId(GetOfficeByCityIdReqDTO getOfficeByCityIdReqDTO)
        {
            return _officeBLL.GetOfficeByCityId(getOfficeByCityIdReqDTO);
        }

        public CommonResponse GetOfficeDetailById(GetOfficeReqDTO getOfficeReqDTO)
        {
            return _officeBLL.GetOfficeDetailById(getOfficeReqDTO);
        }

        public CommonResponse AddOffice(AddOfficeReqDTO addOfficeReqDTO)
        {
            return _officeBLL.AddOffice(addOfficeReqDTO);
        }

        public CommonResponse UpdateOffice(UpdateOfficeReqDTO updateOfficeReqDTO)
        {
            return _officeBLL.UpdateOffice(updateOfficeReqDTO);
        }

        public CommonResponse DeleteOffice(DeleteOfficeReqDTO deleteOfficeReqDTO)
        {
            return _officeBLL.DeleteOffice(deleteOfficeReqDTO);
        }
    }
}
