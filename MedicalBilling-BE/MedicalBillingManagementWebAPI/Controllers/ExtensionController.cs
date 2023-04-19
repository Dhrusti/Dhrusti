using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        private readonly IExtension _extension;

        public ExtensionController(IExtension extension)
        {
            _extension = extension;
        }

        [HttpGet("GetAllExtension")]
        public CommonResponse GetExtensionList()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _extension.GetExtensionList();
                List<GetAllExtensionResDTO> model = commonResponse.Data;
                commonResponse.Data = model.Adapt<List<GetAllExtensionResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }
    }
}
