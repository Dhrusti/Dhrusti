using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class ImplCar : ICar
    {
        private readonly CarBLL _carBLL;

        public ImplCar(CarBLL carBLL)
        {
            _carBLL = carBLL;
        }

        public CommonResponse GetCars()
        {
            return _carBLL.GetCars();
        }

        public CommonResponse AddCars(AddCarReqDTO addCarReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _carBLL.AddCars(addCarReqDTO);
            return commonResponse;
        }

        public CommonResponse UpdateCar(UpdateCarReqDTO updateCarReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _carBLL.UpdateCar(updateCarReqDTO);
            return commonResponse;
        }

        public CommonResponse DeleteCar(DeleteCarReqDTO deleteCarReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _carBLL.DeleteCar(deleteCarReqDTO);
            return commonResponse;
        }

    }
}
