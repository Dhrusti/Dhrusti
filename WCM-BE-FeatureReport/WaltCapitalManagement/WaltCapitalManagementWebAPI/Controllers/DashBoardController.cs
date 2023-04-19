using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoard _iDashBoard;
        public DashBoardController(IDashBoard iDashBoard)
        {
           _iDashBoard = iDashBoard;
        }

        [HttpPost("GetDashBoardWaltValuation")]
        public CommonResponse GetDashBoardWaltValuation()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iDashBoard.GetDashBoardWaltValuation();
                GetDashBoardWaltValuationResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetDashBoardWaltValuationResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetDashBoardOffice")]
        public CommonResponse GetDashBoardOffice()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iDashBoard.GetDashBoardOffice();
               GetDashBoardOfficeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetDashBoardOfficeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
