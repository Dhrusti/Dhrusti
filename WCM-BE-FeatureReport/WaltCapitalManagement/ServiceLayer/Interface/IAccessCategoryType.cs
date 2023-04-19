using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IAccessCategoryType
    {
        public CommonResponse AddAccessCategoryType(AccessCategoryTypeReqDTO accessCategoryTypeReqDTO);
        public CommonResponse DeleteAccessCategoryType(DeleteAccessCategoryTypeReqDTO deleteAccessCategoryTypeReqDTO);
    }
}
