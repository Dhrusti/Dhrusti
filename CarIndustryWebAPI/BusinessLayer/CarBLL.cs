using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public class CarBLL
    {
        private readonly CarIndustryDBContext _dBContext;
        private readonly CommonRepo _commonRepo;

        public CarBLL(CarIndustryDBContext dBContext, CommonRepo commonRepo)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
        }

        public CommonResponse GetCars()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var carList = _commonRepo.carList().ToList();
                if (carList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                    commonResponse.Message = "Success.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    commonResponse.Message = "Data not Found.";
                }
                commonResponse.Data = carList.Adapt<List<GetCarsResDTO>>();
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Data = ex;
            }
            return commonResponse;
        }

        public CommonResponse AddCars(AddCarReqDTO addCarReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddCarResDTO addCarResDTO = new AddCarResDTO();
            try
            {
                var cars = _commonRepo.carList();
                if (cars != null)
                {
                    var brand = _commonRepo.brandList().Where(x => x.Id == addCarReqDTO.Brand);
                    if(brand != null)
                    {
                        CarMst carMst = new CarMst();
                        carMst.Model = addCarReqDTO.Model;
                        carMst.RegistrationId = addCarReqDTO.RegistrationId;
                        carMst.Price = addCarReqDTO.Price;
                        carMst.Brand = addCarReqDTO.Brand;
                        carMst.BuyTime = addCarReqDTO.BuyTime;

                        carMst.CreatedBy = addCarReqDTO.UserId;
                        carMst.UpdatedBy = addCarReqDTO.UserId;
                        carMst.CreatedDate = DateTime.Now;
                        carMst.UpdatedDate = DateTime.Now;
                        carMst.IsActive = true;
                        carMst.IsDeleted = false;

                        _dBContext.CarMsts.Add(carMst);
                        _dBContext.SaveChanges();

                        addCarResDTO.CarId = carMst.Id;
                        addCarResDTO.Model = carMst.Model;

                        commonResponse.Message = "Car added Successfully...!!!";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                        commonResponse.Data = addCarResDTO;
                    }
                   
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                    commonResponse.Message = "Can not add Car of this brand...!!!";
                }


            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Data = ex;
            }
            return commonResponse;
        }

        public CommonResponse UpdateCar(UpdateCarReqDTO updateCarReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateCarResDTO updateCarResDTO = new UpdateCarResDTO();
            try
            {
                var carDetail = _commonRepo.carList().FirstOrDefault(x => x.Id == updateCarReqDTO.Id);
                if (carDetail != null)
                {
                    CarMst carMst = carDetail;
                    carMst.Model = updateCarReqDTO.Model;
                    carMst.Brand = updateCarReqDTO.Brand;
                    carMst.RegistrationId = updateCarReqDTO.RegistrationId;
                    carMst.Price = updateCarReqDTO.Price;
                    carMst.BuyTime = updateCarReqDTO.BuyTime;
                    carMst.UpdatedBy = updateCarReqDTO.UserId;
                    carMst.UpdatedDate = DateTime.Now;

                    _dBContext.Entry(carMst).State = EntityState.Modified;
                    _dBContext.SaveChanges();

                    updateCarResDTO.Model = carMst.Model;
                    updateCarResDTO.Brand = carMst.Brand;
                    updateCarResDTO.RegistrationId = carMst.RegistrationId;
                    updateCarResDTO.Price = carMst.Price;
                    updateCarResDTO.BuyTime = carMst.BuyTime;

                    commonResponse.Data = updateCarResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                    commonResponse.Message = "Successfully Updated...!!!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                    commonResponse.Message = "Can not update Category...!!!";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Data = ex;
            }
            return commonResponse;
        }


        public CommonResponse DeleteCar(DeleteCarReqDTO deleteCarReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteCarResDTO deleteCarResDTO = new DeleteCarResDTO();
            try
            {

                var car = _commonRepo.carList().FirstOrDefault(x => x.Id == deleteCarReqDTO.Id);
                if (car != null)
                {
                    CarMst carMst = car;
                    carMst.Id = deleteCarReqDTO.Id;
                    carMst.UpdatedBy = deleteCarReqDTO.UserId;
                    carMst.IsDeleted = true;
                    carMst.UpdatedDate = DateTime.Now;

                    _dBContext.Entry(carMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dBContext.SaveChanges();

                    deleteCarResDTO.Id = carMst.Id;

                    commonResponse.Data = deleteCarResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                    commonResponse.Message = "Deleted Successfully...!!!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                    commonResponse.Message = "Can not delete car...!!!";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Data = ex.ToString();
            }
            return commonResponse;
        }

    }
}
