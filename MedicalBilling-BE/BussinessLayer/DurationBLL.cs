using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class DurationBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;

        public DurationBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;

        }

        public CommonResponse AddDuration(AddDurationReqDTO addDurationReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddDurationResDTO addDurationResDTO = new AddDurationResDTO();
            try
            {
                DurationMst duration = new DurationMst();
                var list = _dbContext.DurationMsts.OrderBy(x => x.AppointmentId).LastOrDefault();
           

                    if (addDurationReqDTO.Status != null)
                    {
                        if (addDurationReqDTO.Status == "Start" || addDurationReqDTO.Status == "Resume")
                        {
                            duration.AppointmentId = addDurationReqDTO.AppointmentId;
                        duration.StartDate = addDurationReqDTO.Status == "Start" || addDurationReqDTO.Status == "Resume" ? DateTime.Now : duration.StartDate;
                            duration.EndDate = duration.EndDate;
                            duration.IsActive = true;
                            duration.IsDeleted = false;
                            duration.CreatedBy = addDurationReqDTO.CreatedBy;
                            duration.UpdatedBy = addDurationReqDTO.CreatedBy;
                            duration.CreatedDate = DateTime.Now;
                            duration.UpdatedDate = DateTime.Now;

                            _dbContext.DurationMsts.Add(duration);
                            _dbContext.SaveChanges();

                            addDurationResDTO.AppointmentId = duration.AppointmentId;
                            addDurationResDTO.Status = addDurationReqDTO.Status;
                        }
                        else if (addDurationReqDTO.AppointmentId == list.AppointmentId)
                        {
                            list.EndDate = addDurationReqDTO.Status == "Hold" ? DateTime.Now : list.EndDate;

                            _dbContext.DurationMsts.Update(list);
                            _dbContext.SaveChanges();

                            addDurationResDTO.AppointmentId = list.AppointmentId;
                            addDurationResDTO.Status = addDurationReqDTO.Status;
                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Status = false;
                            commonResponse.Message = "Fail";

                        }
                    }
                    else
                    {
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Status = false;
                        commonResponse.Message = "Fail";

                    }


                if (addDurationResDTO != null)
                {
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Status = true;
                    commonResponse.Message = "Success";
                    commonResponse.Data = addDurationResDTO;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Status = false;
                    commonResponse.Message = "Fail";

                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
