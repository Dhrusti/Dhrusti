using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class AccessCategoryImpl : IAccessCategory
    {
        private readonly AccessCategoryBLL _accessCategoryBLL;
        public AccessCategoryImpl(AccessCategoryBLL accessCategoryBLL)
        {
            _accessCategoryBLL = accessCategoryBLL;
        }

        public CommonResponse AddAccessCategory(AccessCategoryReqDTO accessCategoryReqDTO)
        {
            return _accessCategoryBLL.AddAccessCategory(accessCategoryReqDTO);
        }

        public CommonResponse DeleteAccessCategory(DeleteAccessCategoryReqDTO deleteAccessCategoryReqDTO)
        {
            return _accessCategoryBLL.DeleteAccessCategory(deleteAccessCategoryReqDTO);
        }
    }
}
