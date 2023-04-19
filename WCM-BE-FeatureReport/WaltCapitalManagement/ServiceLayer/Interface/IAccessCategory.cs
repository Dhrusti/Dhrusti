using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IAccessCategory
    {
        public CommonResponse AddAccessCategory(AccessCategoryReqDTO AccessCategoryReqDTO);
        public CommonResponse DeleteAccessCategory(DeleteAccessCategoryReqDTO DeleteAccessCategoryReqDTO);
    }
}
