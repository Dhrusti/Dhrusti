using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using System.Net;

namespace BusinessLayer
{
    public class PersonalityTypeBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public PersonalityTypeBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse AddPersonalityType(AddPersonalityTypeReqDTO addPersonalityTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(addPersonalityTypeReqDTO.PersonalityType) && (addPersonalityTypeReqDTO.PersonalityType != ""))
                {
                    if (!IsPersonalityTypeExists(addPersonalityTypeReqDTO.PersonalityType))
                    {
                        PersonalityTypeMst personalityTypeMst = new PersonalityTypeMst();
                        personalityTypeMst.PersonalityType = addPersonalityTypeReqDTO.PersonalityType;
                        personalityTypeMst.CreatedBy = addPersonalityTypeReqDTO.UserId;
                        personalityTypeMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                        personalityTypeMst.UpdatedBy = addPersonalityTypeReqDTO.UserId;
                        personalityTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        personalityTypeMst.IsActive = true;
                        personalityTypeMst.IsDeleted = false;
                        _dbContext.PersonalityTypeMsts.Add(personalityTypeMst);
                        var result = _dbContext.SaveChanges();
                        if (result > 0)
                        {
                            AddPersonalityTypeResDTO addPersonalityTypeResDTO = new();
                            addPersonalityTypeResDTO.Id = personalityTypeMst.Id;
                            addPersonalityTypeResDTO.PersonalityType = personalityTypeMst.PersonalityType;

                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "PersonalityType added successfully";
                            commonResponse.Data = addPersonalityTypeResDTO;
                        }
                        else
                        {
                            commonResponse.Message = "Fail to add PersonalityType";
                        }
                    }
                    else
                    {
                        commonResponse.Message = "PersonalityType already exist";
                    }
                }
                else
                {
                    commonResponse.Message = "Enter valid PersonalityType";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdatePersonalityType(UpdatePersonalityTypeReqDTO updatePersonalityTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(updatePersonalityTypeReqDTO.PersonalityType))
                {
                    PersonalityTypeMst personalityTypeMst = _commonRepo.personalityType().FirstOrDefault(x => x.Id == updatePersonalityTypeReqDTO.Id);
                    if (personalityTypeMst != null)
                    {
                        if (!IsPersonalityTypeExists(updatePersonalityTypeReqDTO.PersonalityType))
                        {
                            personalityTypeMst.PersonalityType = updatePersonalityTypeReqDTO.PersonalityType;
                            personalityTypeMst.UpdatedBy = updatePersonalityTypeReqDTO.UserId;
                            personalityTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                            _dbContext.Entry(personalityTypeMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            var result = _dbContext.SaveChanges();
                            if (result > 0)
                            {
                                UpdatePersonalityTypeResDTO updatePersonalityTypeResDTO = new();
                                updatePersonalityTypeResDTO.Id = personalityTypeMst.Id;
                                updatePersonalityTypeResDTO.PersonalityType = personalityTypeMst.PersonalityType;



                                commonResponse.Message = "PersonalityType updated successfully";
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Data = updatePersonalityTypeResDTO;

                            }
                            else
                            {
                                commonResponse.Message = "Fail to update PersonalityType";
                            }

                        }
                        else
                        {
                            commonResponse.Message = "PersonalityType already exist";
                        }
                    }
                    else
                    {
                        commonResponse.Message = "PersonalityType not found";
                    }
                }
                else
                {
                    commonResponse.Message = "Enter valid PersonalityType ";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeletePersonalityType(DeletePersonalityTypeReqDTO deletePersonalityTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                PersonalityTypeMst personalityTypeMst = _commonRepo.personalityType().FirstOrDefault(x => x.Id == deletePersonalityTypeReqDTO.Id);
                if (personalityTypeMst != null)
                {
                    personalityTypeMst.IsDeleted = true;
                    personalityTypeMst.UpdatedBy = deletePersonalityTypeReqDTO.UserId;
                    personalityTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(personalityTypeMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    var result = _dbContext.SaveChanges();
                    if (result > 0)
                    {
                        commonResponse.Message = "PersonalityType Deleted successfully";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;

                    }
                    else
                    {
                        commonResponse.Message = "Fail to delete PersonalityType";
                    }
                }
                else
                {
                    commonResponse.Message = "PersonalityType not found";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllPersonalityType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetPersonalityTypeResDTO> getPersonalityTypeResDTOs = _commonRepo.personalityType().ToList().Select(x => new GetPersonalityTypeResDTO
                {
                    Id = x.Id,
                    PersonalityType = x.PersonalityType
                }).ToList();
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "PersonalityType found";
                commonResponse.Data = getPersonalityTypeResDTOs;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Data = ex;
            }
            return commonResponse;
        }

        public CommonResponse GetPersonalityTypeById(GetByIdPersonalityTypeReqDTO getByIdPersonalityTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetPersonalityTypeResDTO getPersonalityTypeResDTOs = _commonRepo.personalityType().Where(x => x.Id == getByIdPersonalityTypeReqDTO.Id).Select(x => new GetPersonalityTypeResDTO
                {
                    Id = x.Id,
                    PersonalityType = x.PersonalityType
                }).FirstOrDefault();

                //List<GetPersonalityTypeResDTO  = _commonRepo.personalityType();
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "PersonalityType found";
                commonResponse.Data = getPersonalityTypeResDTOs;
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        private bool IsPersonalityTypeExists(string PersonalityType)
        {
            return _commonRepo.personalityType().Where(x => x.PersonalityType.ToLower() == PersonalityType.ToLower()).Any();
        }
    }
}
