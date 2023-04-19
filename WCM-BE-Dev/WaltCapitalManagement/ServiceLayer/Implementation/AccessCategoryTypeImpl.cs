using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class AccessCategoryTypeImpl : IAccessCategoryType
    {
        private readonly AccessCategoryTypeBLL _iaccessCategoryType;
        public AccessCategoryTypeImpl(AccessCategoryTypeBLL iaccessCategoryType)
        {
            _iaccessCategoryType = iaccessCategoryType;
        }

        public CommonResponse AddAccessCategoryType(AccessCategoryTypeReqDTO accessCategoryTypeReqDTO)
        {
            return _iaccessCategoryType.AddAccessCategoryType(accessCategoryTypeReqDTO);
        }
        public CommonResponse DeleteAccessCategoryType(DeleteAccessCategoryTypeReqDTO deleteAccessCategoryTypeReqDTO)
        {
            return _iaccessCategoryType.DeleteAccessCategoryType(deleteAccessCategoryTypeReqDTO);
        }
    }
}
