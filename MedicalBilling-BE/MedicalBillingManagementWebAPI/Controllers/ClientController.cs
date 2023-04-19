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
    public class ClientController : ControllerBase
    {
        private readonly IClient _client;

        public ClientController(IClient client)
        { 
            _client = client;
        }

        [HttpGet("GetAllClient")]
        public CommonResponse GetAllClient()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _client.GetAllClient();
                List<GetAllClientResDTO> model = commonResponse.Data;
                commonResponse.Data = model.Adapt<List<GetAllClientResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
          
            
        }
    }
}
