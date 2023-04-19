using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using ERP_CRM.ViewModels.ReqViewModel;
using ERP_CRM.ViewModels.ResViewModel;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace ERP_CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequirementController : ControllerBase
    {
        private readonly ErpDbContext _dbContext;
        private readonly IRequirement _iRequirement;
        public RequirementController(IRequirement iRequirement, ErpDbContext dbContext)
        {
            _iRequirement = iRequirement;
            _dbContext = dbContext;
        }

        [HttpPost("GetAllRequirement")]
        public CommonResponse GetAllRequirement()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iRequirement.GetAllRequirement();
                List<GetRequirementResDTO> getRequirementResDTO = commonResponse.Data;
                commonResponse.Data = getRequirementResDTO.Adapt<List<GetRequirementResViewModel>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [HttpPost("GetRequirementById")]
        public CommonResponse GetRequirementById(GetRequirementReqViewModel getRequirementReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iRequirement.GetRequirementById(getRequirementReqViewModel.Adapt<GetRequirementReqDTO>());
                GetRequirementResDTO getRequirementResDTO = commonResponse.Data;
                commonResponse.Data = getRequirementResDTO.Adapt<GetRequirementResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [HttpPost("AddRequirements")]
        public CommonResponse AddRequirements(AddRequirementsReqViewModel addRequirementsReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iRequirement.AddRequirements(addRequirementsReqViewModel.Adapt<AddRequirementsReqDTO>());
                AddRequirementsResDTO addRequirementsResDTO = commonResponse.Data;
                commonResponse.Data = addRequirementsResDTO.Adapt<AddRequirementsResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
