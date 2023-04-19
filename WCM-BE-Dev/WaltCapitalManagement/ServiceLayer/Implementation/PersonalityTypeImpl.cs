using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class PersonalityTypeImpl : IPersonalityType
    {
        private readonly PersonalityTypeBLL _personalityTypeBLL;

        public CommonResponse GetPersonalityTypeById(GetByIdPersonalityTypeReqDTO getByIdPersonalityTypeReqDTO)
        {
            return _personalityTypeBLL.GetPersonalityTypeById(getByIdPersonalityTypeReqDTO);
        }

        public CommonResponse UpdatePersonalityType(UpdatePersonalityTypeReqDTO updatePersonalityTypeReqDTO)
        {
            return _personalityTypeBLL.UpdatePersonalityType(updatePersonalityTypeReqDTO);
        }

        public PersonalityTypeImpl(PersonalityTypeBLL personalityTypeBLL)
        {
            _personalityTypeBLL = personalityTypeBLL;
        }
        public CommonResponse AddPersonalityType(AddPersonalityTypeReqDTO addPersonalityTypeReqDTO)
        {
            return _personalityTypeBLL.AddPersonalityType(addPersonalityTypeReqDTO);
        }

        public CommonResponse DeletePersonalityType(DeletePersonalityTypeReqDTO deletePersonalityTypeReqDTO)
        {
            return _personalityTypeBLL.DeletePersonalityType(deletePersonalityTypeReqDTO);
        }

        public CommonResponse GetAllPersonalityType()
        {
            return _personalityTypeBLL.GetAllPersonalityType();
        }
    }
}
