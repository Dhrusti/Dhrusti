using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface ICar
    {
        public CommonResponse GetCars();

        public CommonResponse AddCars(AddCarReqDTO addCarReqDTO);

        public CommonResponse UpdateCar(UpdateCarReqDTO updateCarReqDTO);

        public CommonResponse DeleteCar(DeleteCarReqDTO deleteCarReqDTO);
    }
}
