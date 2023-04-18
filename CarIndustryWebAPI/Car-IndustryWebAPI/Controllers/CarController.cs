using Car_IndustryWebAPI.ViewModels.ReqViewModels;
using Car_IndustryWebAPI.ViewModels.ResViewModels;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace Car_IndustryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICar _icar;

        public CarController(ICar icar)
        {
            _icar = icar;
        }


        [HttpGet("GetCars")]
        public CommonResponse GetCars()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _icar.GetCars();
                List<GetCarsResDTO> getCarsResDTO = commonResponse.Data ?? new List<GetCarsResDTO>();
                commonResponse.Data = getCarsResDTO.Adapt<List<GetCarsResViewModel>>();
            }
            catch(Exception ex)
            {
                commonResponse.Data = ex;
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

        [HttpPost("AddCars")]
        public CommonResponse AddCars(AddCarReqViewModel addCarReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _icar.AddCars(addCarReqViewModel.Adapt<AddCarReqDTO>());
                AddCarResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddCarResViewModel>();
            }
            catch(Exception ex)
            {
                commonResponse.Data = ex;
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

        [HttpPut("UpdateCar")]
        public CommonResponse UpdateCar(UpdateCarReqViewModel updateCarReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (updateCarReqViewModel.Id != null)
                    commonResponse = _icar.UpdateCar(updateCarReqViewModel.Adapt<UpdateCarReqDTO>());
                UpdateCarResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateCarResViewModel>();
            }
            catch (Exception ex)
            {
                commonResponse.Data = ex;
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

        [HttpDelete("DeleteCar")]
        public CommonResponse DeleteCar(DeleteCarReqViewModel deleteCarReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (deleteCarReqViewModel.Id != null)
                    commonResponse = _icar.DeleteCar(deleteCarReqViewModel.Adapt<DeleteCarReqDTO>());
                DeleteCarResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteCarResViewModel>();
            }
            catch (Exception ex)
            {
                commonResponse.Data = ex;
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

    }
}
