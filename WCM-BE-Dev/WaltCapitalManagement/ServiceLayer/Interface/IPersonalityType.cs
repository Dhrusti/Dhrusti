using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IPersonalityType
    {
        public CommonResponse GetAllPersonalityType();

        public CommonResponse GetPersonalityTypeById(GetByIdPersonalityTypeReqDTO getByIdPersonalityTypeReqDTO);

        public CommonResponse AddPersonalityType(AddPersonalityTypeReqDTO addPersonalityTypeReqDTO);

        public CommonResponse UpdatePersonalityType(UpdatePersonalityTypeReqDTO updatePersonalityTypeReqDTO);

        public CommonResponse DeletePersonalityType(DeletePersonalityTypeReqDTO deletePersonalityTypeReqDTO);
    }
}
