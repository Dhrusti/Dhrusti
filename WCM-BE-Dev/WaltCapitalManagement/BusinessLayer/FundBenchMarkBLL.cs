using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static DTO.ReqDTO.UpdateFundBenchMarkReqDTO;
using System.Transactions;

namespace BusinessLayer
{
    public class FundBenchMarkBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _configaration;
        private readonly CommonHelper _commonHelper;

        public FundBenchMarkBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configaration)

        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configaration = configaration;
        }

        public CommonResponse GetAllFundBenchMark(GetFundBenchMarkReqDTO getFundBenchMarkReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            var pageData = _configaration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configaration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configaration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);


            int number = getFundBenchMarkReqDTO.PageNumber == 0 ? (pagecount) : getFundBenchMarkReqDTO.PageNumber;
            int size = getFundBenchMarkReqDTO.PageSize == 0 ? (pageSize) : getFundBenchMarkReqDTO.PageSize;
            bool orderby = getFundBenchMarkReqDTO.Orderby == true ? (order) : getFundBenchMarkReqDTO.Orderby;
            try
            {
                GetFundBenchMarkResDTO getFundBenchMarkResDTO = new GetFundBenchMarkResDTO();
                List<FundBenchMarkList> fundBenchMarkList = new List<FundBenchMarkList>();

                fundBenchMarkList = _commonRepo.FundBenchMarkList().Where(x => x.FundId == getFundBenchMarkReqDTO.FundId && x.IsAddMode == true).Select(x => new FundBenchMarkList
                {
                    Id = x.Id,
                    BenchMarkName = x.BenchMarkName,
                    BenchMarkValue = (x.BenchMarkValue).ToString("0.00"),
                    BenchMarkDate = x.BenchMarkDate,
                    IsInDashboard = x.IsInDashboard,
                    IsRemoveMode = x.IsDeleted
                    //  Status = x.IsAddMode == true ? "Active" : "InActive",
                }).ToList();

                getFundBenchMarkResDTO.TotalCount = fundBenchMarkList.Count();

                if (getFundBenchMarkReqDTO.Alphabet != null && !string.IsNullOrWhiteSpace(getFundBenchMarkReqDTO.Alphabet))
                {
                    fundBenchMarkList = fundBenchMarkList.Where(x => x.BenchMarkName.ToLower().StartsWith(getFundBenchMarkReqDTO.Alphabet.ToLower())).ToList();

                }

                if (getFundBenchMarkReqDTO.SearchString != null && !string.IsNullOrEmpty(getFundBenchMarkReqDTO.SearchString))
                {
                    fundBenchMarkList = fundBenchMarkList.Where(x => x.BenchMarkName.ToLower().Contains(getFundBenchMarkReqDTO.SearchString.ToLower()) || x.BenchMarkName.ToLower().Contains(getFundBenchMarkReqDTO.SearchString.ToLower())).ToList();

                }

                if (orderby)
                {
                    if (fundBenchMarkList.Count <= size)
                    {
                        fundBenchMarkList = fundBenchMarkList.OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        fundBenchMarkList = fundBenchMarkList.Skip((number - 1) * size)
                                .Take(size)
                                .OrderBy(x => x.Id)
                                .ToList();
                    }
                }
                else
                {
                    if (fundBenchMarkList.Count <= size)
                    {
                        fundBenchMarkList = fundBenchMarkList.OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        fundBenchMarkList = fundBenchMarkList.OrderByDescending(x => x.Id).Skip((number - 1) * size)
                            .Take(size)
                            .ToList();
                    }
                }

                getFundBenchMarkResDTO.fundBenchMarkList = fundBenchMarkList;

                if (fundBenchMarkList != null)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = getFundBenchMarkResDTO;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Data not found";
                    commonResponse.Data = getFundBenchMarkResDTO;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }
        public CommonResponse GetAllUpdateFundBenchMark(GetAllUpdateFundBenchMarkReqDTO getAllUpdateFundBenchMarkReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var fundBenchMarkUpdateList = _commonRepo.FundBenchMarkList().Where(x => x.FundId == getAllUpdateFundBenchMarkReqDTO.FundId && x.IsAddMode == true).Select(x => new GetAllUpdateFundBenchMarkResDTO
                {
                    Id = x.Id,
                    FundId = x.FundId,
                    Label = x.BenchMarkName,
                    Value = "",
                    IsInDashboard = x.IsInDashboard,
                    IsRemoveMode = x.IsDeleted,
                }).ToList();

                if (fundBenchMarkUpdateList != null)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = fundBenchMarkUpdateList;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not found";
                    commonResponse.Data = fundBenchMarkUpdateList;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }
        public CommonResponse AddFundBenchMark(AddFundBenchMarkReqDTO addFundBenchMarkReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddFundBenchMarkResDTO addFundBenchMarkResDTO = new AddFundBenchMarkResDTO();
            try
            {

                var IsAbsoluteBenchMark = _commonRepo.FundBenchMarkList().Where(x => x.BenchMarkName.ToLower() == addFundBenchMarkReqDTO.BenchMarkName.ToLower() && x.FundId == addFundBenchMarkReqDTO.FundId).ToList();
                if (IsAbsoluteBenchMark.Count == 0)
                {
                    FundBenchMarkMst fundBenchMarkMst = new FundBenchMarkMst();
                    fundBenchMarkMst.BenchMarkName = addFundBenchMarkReqDTO.BenchMarkName;
                    fundBenchMarkMst.BenchMarkValue = addFundBenchMarkReqDTO.BenchMarkValue;
                    fundBenchMarkMst.BenchMarkDate = addFundBenchMarkReqDTO.BenchMarkDate;
                    fundBenchMarkMst.FundId = addFundBenchMarkReqDTO.FundId;
                    fundBenchMarkMst.IsActive = true;
                    fundBenchMarkMst.IsDeleted = false;
                    fundBenchMarkMst.IsAddMode = true;
                    fundBenchMarkMst.IsInDashboard = false;
                    fundBenchMarkMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    fundBenchMarkMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dBContext.FundBenchMarkMsts.Add(fundBenchMarkMst);
                    _dBContext.SaveChanges();

                    addFundBenchMarkResDTO.Id = fundBenchMarkMst.Id;
                    addFundBenchMarkResDTO.BenchMarkName = fundBenchMarkMst.BenchMarkName;
                    addFundBenchMarkResDTO.BenchMarkValue = fundBenchMarkMst.BenchMarkValue;
                    addFundBenchMarkResDTO.BenchMarkDate = fundBenchMarkMst.BenchMarkDate.Date;


                    commonResponse.Message = "BenchMark added successfully!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addFundBenchMarkResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "BenchMark Name Already Exist!";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }
        public CommonResponse UpdateFundBenchMark(UpdateFundBenchMarkReqDTO updateFundBenchMarkReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateFundBenchMarkResDTO updateFundBenchMarkResDTO = new UpdateFundBenchMarkResDTO();
            try
            {
                using (var scope = new TransactionScope())
                {
                    bool isScopeDis = true;
                    BenchMarks benchMarks = new BenchMarks();
                    var updatefundBenchMark = _commonRepo.FundBenchMarkList();

                    var IsAbsoluteBenchMark = _commonRepo.FundBenchMarkList().Where(x => x.BenchMarkName.ToLower() == benchMarks.Label.ToLower() && x.Id == benchMarks.Id);
                    FundBenchMarkMst fundBenchMarkMst;

                    foreach (var item in updateFundBenchMarkReqDTO.benchmarks)
                    {
                        var fundbenchmarklist = _dBContext.FundBenchMarkMsts.Where(x => x.Id == item.Id).FirstOrDefault();
                        if (IsAbsoluteBenchMark != null)
                        {
                            if (fundbenchmarklist != null)
                            {
                                fundbenchmarklist.IsAddMode = false;
                                fundbenchmarklist.IsInDashboard = false;
                                _dBContext.Entry(fundbenchmarklist).State = EntityState.Modified;
                                _dBContext.SaveChanges();

                            }
                            fundBenchMarkMst = new FundBenchMarkMst();
                            fundBenchMarkMst.BenchMarkName = item.Label;
                            fundBenchMarkMst.BenchMarkValue = (!String.IsNullOrWhiteSpace(Convert.ToString(item.Value))) ? Convert.ToDecimal(item.Value) : fundbenchmarklist.BenchMarkValue ;
                            fundBenchMarkMst.BenchMarkDate = updateFundBenchMarkReqDTO.Date;
                            fundBenchMarkMst.FundId = updateFundBenchMarkReqDTO.FundId;
                            fundBenchMarkMst.IsActive = true;
                            fundBenchMarkMst.IsDeleted = item.IsRemoveMode;
                            fundBenchMarkMst.IsAddMode = true;
                            fundBenchMarkMst.IsInDashboard = item.IsInDashboard;
                            fundBenchMarkMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                            fundBenchMarkMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                            _dBContext.FundBenchMarkMsts.Add(fundBenchMarkMst);
                            _dBContext.SaveChanges();
                   
                            updateFundBenchMarkResDTO.FundId = updateFundBenchMarkReqDTO.FundId;

                            isScopeDis = true;

                        }
                        else
                        {
                            isScopeDis = false;
                            
                        }
                     
                    }
                    if (isScopeDis)
                    {
                        scope.Complete();
                        commonResponse.Message = "BenchMark Value Updated successfully!";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = updateFundBenchMarkResDTO;

                    }
                    else
                    {
                        scope.Dispose();
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "BenchMark Name Already Exist!";
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }
        public CommonResponse UpdateAddStatusFundBenchMark(UpdateStatusFundBenchMarkReqDTO updateStatusFundBenchMarkReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateStatusFundBenchMarkResDTO updateStatusFundBenchMarkResDTO = new UpdateStatusFundBenchMarkResDTO();
            try
            {
                var fundBenchMark = _commonRepo.FundBenchMarkList().FirstOrDefault(x => x.Id == updateStatusFundBenchMarkReqDTO.Id);
                if (fundBenchMark != null)
                {
                    fundBenchMark.IsInDashboard = updateStatusFundBenchMarkReqDTO.IsInDashBoard;
                    if (updateStatusFundBenchMarkReqDTO.IsInDashBoard == true)
                    {
                        fundBenchMark.IsDeleted = false;
                    }
                    _dBContext.Entry(fundBenchMark).State = EntityState.Modified;
                    _dBContext.SaveChanges();

                    updateStatusFundBenchMarkResDTO.Id = fundBenchMark.Id;
                    updateStatusFundBenchMarkResDTO.IsInDashBoard = fundBenchMark.IsInDashboard;

                    commonResponse.Data = updateStatusFundBenchMarkResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "BenchMark Added Successfully!";
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found.";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }
        public CommonResponse UpdateRemoveStatusFundBenchMark(UpdateRemoveStatusFundBenchMarkReqDTO updateRemoveStatusFundBenchMarkReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateRemoveStatusFundBenchMarkResDTO updateRemoveStatusFundBenchMarkResDTO = new UpdateRemoveStatusFundBenchMarkResDTO();
            try
            {
                var fundBenchMark = _commonRepo.FundBenchMarkList().FirstOrDefault(x => x.Id == updateRemoveStatusFundBenchMarkReqDTO.Id);
                if (fundBenchMark != null)
                {
                    fundBenchMark.IsDeleted = updateRemoveStatusFundBenchMarkReqDTO.IsRemoveMode;
                    if (updateRemoveStatusFundBenchMarkReqDTO.IsRemoveMode == true)
                    {
                        fundBenchMark.IsInDashboard = false;
                    }

                    _dBContext.Entry(fundBenchMark).State = EntityState.Modified;
                    _dBContext.SaveChanges();

                    updateRemoveStatusFundBenchMarkResDTO.Id = fundBenchMark.Id;
                    updateRemoveStatusFundBenchMarkResDTO.IsRemoveMode = fundBenchMark.IsDeleted;

                    commonResponse.Data = updateRemoveStatusFundBenchMarkResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "BenchMark Remove Successfully!";
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found.";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }
        public CommonResponse GetAllDashboarFundBenchMark(GetAllDashboardFundBenchMarkReqDTO getAllDashboardFundBenchMarkReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();

            try
            {
                var fundBenchMarkDashboardList = _commonRepo.FundBenchMarkList().Where(x => x.FundId == getAllDashboardFundBenchMarkReqDTO.FundId && x.IsInDashboard == true && x.IsDeleted == false && x.IsAddMode == true).Select(x => new GetAllFundBenchMarkDashboardResDTO
                {
                    BenchMarkId = x.Id,
                    BenchMarkName = x.BenchMarkName
                }).ToList();

                if (fundBenchMarkDashboardList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = fundBenchMarkDashboardList;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Data not found";
                    commonResponse.Data = fundBenchMarkDashboardList;
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
