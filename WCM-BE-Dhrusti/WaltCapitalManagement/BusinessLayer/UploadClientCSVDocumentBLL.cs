using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using ExcelDataReader;
using Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;
using System.Transactions;

namespace BusinessLayer
{
    public class UploadClientCSVDocumentBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonHelper _commonHelper;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _configaration;
        private IHostingEnvironment _hostingEnvironment { get; }

        public UploadClientCSVDocumentBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _hostingEnvironment = hostingEnvironment;
            _configaration = configuration;
        }

        public CommonResponse UploadClientCSVDocument(UploadClientCSVDocumentReqDTO uploadClientCSVDocumentReqDTO)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                int ErrorCount = 0;
                DataSet dsExcelRecords = new DataSet();
                IExcelDataReader reader = null;

                var files = uploadClientCSVDocumentReqDTO.File;
                FileInfo fileInfo = new FileInfo(files.FileName);
                string csvFileName = files.FileName;
                string FileExtension = fileInfo.Extension.ToLower();
                long FileSize = files.Length;
                bool validateFileExtension = false;
                bool IsFileDuplicate = false;
                string[] allowedFileExtensions = { ".csv" };
                validateFileExtension = allowedFileExtensions.Contains(FileExtension) ? true : false;
                var clientCsvLog = _dBContext.ClientCsvdataLogMsts.FirstOrDefault(x => x.ClientCsvfileName == csvFileName);
                var clientCSVDatalog = _dBContext.ClientCsvdataLogMsts.FirstOrDefault();
                var clientCSVData = _dBContext.ClientCsvdataMsts.ToList();
                if (clientCSVData.Count > 0)
                {
                    IsFileDuplicate = true;

                }
                if (validateFileExtension)
                {

                    ClientCsvdataLogMst csvfileUploadLogMst = new ClientCsvdataLogMst();
                    var UploadFileresponse = _commonHelper.UploadFile(files, "ClientCSVFile", files.FileName);

                    if (UploadFileresponse.Status)
                    {
                        using (TransactionScope transactionScope1 = new TransactionScope())
                        {
                            if (IsFileDuplicate)
                            {

                                _dBContext.ClientCsvdataMsts.RemoveRange(clientCSVData);
                                _dBContext.SaveChanges();

                                _dBContext.ClientCsvdataLogMsts.Remove(clientCSVDatalog);
                                _dBContext.SaveChanges();


                            }

                            csvfileUploadLogMst.ClientCsvfileName = files.FileName;
                            csvfileUploadLogMst.FileSize = Convert.ToString(files.Length);
                            csvfileUploadLogMst.Extension = FileExtension;
                            csvfileUploadLogMst.Path = UploadFileresponse.Data;
                            csvfileUploadLogMst.CreatedBy = uploadClientCSVDocumentReqDTO.UserId;
                            csvfileUploadLogMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                            _dBContext.ClientCsvdataLogMsts.Add(csvfileUploadLogMst);
                            _dBContext.SaveChanges();
                            string FullPath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", UploadFileresponse.Data);

                            var stream = System.IO.File.Open(FullPath, FileMode.Open, FileAccess.Read);
                            reader = ExcelReaderFactory.CreateCsvReader(stream);
                            dsExcelRecords = reader.AsDataSet();
                            reader.Close();

                            if ((dsExcelRecords != null))
                            {
                                //List<CsvdataMst> csvlist = new List<CsvdataMst>();
                                List<ClientCsvdataMst> clientcsvList = new List<ClientCsvdataMst>();
                                DataTable dtUserRecords = dsExcelRecords.Tables[0];
                                //if (dtUserRecords.Rows[0][0].ToString() == "Adviser code" &&
                                //    dtUserRecords.Rows[0][1].ToString() == "Client name" &&
                                //    dtUserRecords.Rows[0][2].ToString() == "ID number/Registration number" &&
                                //    dtUserRecords.Rows[0][3].ToString() == "Client number" &&
                                //    dtUserRecords.Rows[0][4].ToString() == "Product" &&
                                //    dtUserRecords.Rows[0][5].ToString() == "Account Name" &&
                                //    dtUserRecords.Rows[0][6].ToString() == "Account Groups" &&
                                //    dtUserRecords.Rows[0][7].ToString() == "Account number" &&
                                //    dtUserRecords.Rows[0][8].ToString() == "Inception date" &&
                                //    dtUserRecords.Rows[0][9].ToString() == "Fund manager" &&
                                //    dtUserRecords.Rows[0][10].ToString() == "Fund name" &&
                                //    dtUserRecords.Rows[0][11].ToString() == "Fund code" &&
                                //    dtUserRecords.Rows[0][12].ToString() == "Initial adviser fee" &&
                                //    dtUserRecords.Rows[0][13].ToString() == "Annual adviser fee" &&
                                //    dtUserRecords.Rows[0][14].ToString() == "Section 14 adviser fee renewal date" &&
                                //    dtUserRecords.Rows[0][15].ToString() == "Monthly debit order" &&
                                //    dtUserRecords.Rows[0][16].ToString() == "Annuity income/Regular withdrawal" &&
                                //    dtUserRecords.Rows[0][17].ToString() == "Annuity income/Regular withdrawal frequency" &&
                                //    dtUserRecords.Rows[0][18].ToString() == "Annuity income anniversary date" &&
                                //    dtUserRecords.Rows[0][19].ToString() == "Account fund allocation" &&
                                //    dtUserRecords.Rows[0][20].ToString() == "Units" &&
                                //    dtUserRecords.Rows[0][21].ToString() == "Unit price(cents) in fund currency" &&
                                //    dtUserRecords.Rows[0][22].ToString() == "Price date" &&
                                //    dtUserRecords.Rows[0][23].ToString() == "Fund currency" &&
                                //    dtUserRecords.Rows[0][24].ToString() == "Market value in fund currency" &&
                                //    dtUserRecords.Rows[0][25].ToString() == "Exchange rate" &&
                                //    dtUserRecords.Rows[0][26].ToString() == "Market value in rands" &&
                                //    dtUserRecords.Rows[0][27].ToString() == "Annuity revision effective month" &&
                                //    dtUserRecords.Rows[0][28].ToString() == "Net capital gain or loss" &&
                                //    dtUserRecords.Rows[0][29].ToString() == "Model portfolio name" &&
                                //    dtUserRecords.Rows[0][30].ToString() == "Dim fee" &&
                                //    dtUserRecords.Rows[0][31].ToString() == "Ric fee" &&
                                //    dtUserRecords.Rows[0][32].ToString() == "Group RA employer")
                                //{
                                if (dtUserRecords.Rows.Count > 0 && dtUserRecords.Rows.Count == 33)
                                {
                                    for (int i = 1; i < dtUserRecords.Rows.Count; i++)
                                    {
                                        CsvdataMst csvDataMst = new CsvdataMst();

                                        if ((!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][0]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][1]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][2]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][3]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][4]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][5]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][6]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][7]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][8]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][9]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][10]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][11]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][12]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][13]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][14]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][15]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][16]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][17]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][18]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][19]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][20]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][21]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][22]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][23]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][24]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][25]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][26]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][27]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][28]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][29]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][30]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][31]))) ||
                                          (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][32]))))

                                        {

                                            ClientCsvdataMst clientcsvdataMst = new ClientCsvdataMst();
                                            clientcsvdataMst.ClientCsvfieldId = csvfileUploadLogMst.Id;
                                            clientcsvdataMst.AdviserCode = dtUserRecords.Rows[i][0].ToString() != "" ? dtUserRecords.Rows[i][0].ToString() : "";
                                            clientcsvdataMst.ClientName = dtUserRecords.Rows[i][1].ToString() != "" ? dtUserRecords.Rows[i][1].ToString() : "";
                                            clientcsvdataMst.RegistrationNumber = dtUserRecords.Rows[i][2].ToString() != "" ? dtUserRecords.Rows[i][2].ToString() : "";
                                            clientcsvdataMst.ClientNumber = dtUserRecords.Rows[i][3].ToString() != "" ? Convert.ToInt32(dtUserRecords.Rows[i][3].ToString()) : Convert.ToInt32("0");
                                            clientcsvdataMst.Product = dtUserRecords.Rows[i][4].ToString() != "" ? dtUserRecords.Rows[i][4].ToString() : "";
                                            clientcsvdataMst.AccountName = dtUserRecords.Rows[i][5].ToString() != "" ? dtUserRecords.Rows[i][5].ToString() : "";
                                            clientcsvdataMst.AccountGroups = dtUserRecords.Rows[i][6].ToString() != "" ? dtUserRecords.Rows[i][6].ToString() : "";
                                            clientcsvdataMst.AccountNumber = dtUserRecords.Rows[i][7].ToString() != "" ? dtUserRecords.Rows[i][7].ToString() : "";
                                            clientcsvdataMst.InceptionDate = dtUserRecords.Rows[i][8].ToString() != "" ? Convert.ToDateTime(dtUserRecords.Rows[i][8].ToString()) : null;
                                            clientcsvdataMst.FundManager = dtUserRecords.Rows[i][9].ToString() != " " ? dtUserRecords.Rows[i][9].ToString() : " ";
                                            clientcsvdataMst.FundName = dtUserRecords.Rows[i][10].ToString() != " " ? dtUserRecords.Rows[i][10].ToString() : " ";
                                            clientcsvdataMst.FundCode = dtUserRecords.Rows[i][11].ToString() != " " ? dtUserRecords.Rows[i][11].ToString() : " ";
                                            clientcsvdataMst.InitialAdvisorFee = dtUserRecords.Rows[i][12].ToString() != "" ? dtUserRecords.Rows[i][12].ToString() : "";
                                            clientcsvdataMst.AnnualAdvisorFee = dtUserRecords.Rows[i][13].ToString() != "" ? dtUserRecords.Rows[i][13].ToString() : "";
                                            clientcsvdataMst.Section14AdvisorFeeRenewal = dtUserRecords.Rows[i][14].ToString() != "" ? Convert.ToDateTime(dtUserRecords.Rows[i][14].ToString()) : null;
                                            clientcsvdataMst.MonthlyDebitOrder = dtUserRecords.Rows[i][15].ToString() != "" ? dtUserRecords.Rows[i][15].ToString() : "";
                                            clientcsvdataMst.AnnuityIncomeRegularWithdrawal = dtUserRecords.Rows[i][16].ToString() != "" ? dtUserRecords.Rows[i][16].ToString() : "";
                                            clientcsvdataMst.AnnuityIncomeRegularWithdrawalFrequency = dtUserRecords.Rows[i][17].ToString() != "" ? dtUserRecords.Rows[i][17].ToString() : "";
                                            clientcsvdataMst.AnnuityIncomeAnniversary = dtUserRecords.Rows[i][18].ToString() != "" ? Convert.ToDateTime(dtUserRecords.Rows[i][18].ToString()) : null;
                                            clientcsvdataMst.AccountFundAllocation = dtUserRecords.Rows[i][19].ToString() != "" ? dtUserRecords.Rows[i][19].ToString() : "";
                                            clientcsvdataMst.Units = dtUserRecords.Rows[i][20].ToString() != "" ? Math.Abs(Convert.ToDouble(dtUserRecords.Rows[i][20].ToString())) : float.Parse("0");
                                            clientcsvdataMst.UnitPriceCents = dtUserRecords.Rows[i][21].ToString() != "" ? Math.Abs(Convert.ToDouble(dtUserRecords.Rows[i][21].ToString())) : float.Parse("0");
                                            clientcsvdataMst.PriceDate = dtUserRecords.Rows[i][22].ToString() != "" ? Convert.ToDateTime(dtUserRecords.Rows[i][22].ToString()) : null;
                                            clientcsvdataMst.FundCurrency = dtUserRecords.Rows[i][23].ToString() != " " ? dtUserRecords.Rows[i][23].ToString() : " ";
                                            clientcsvdataMst.MarketValueInFundCurrency = dtUserRecords.Rows[i][24].ToString() != "" ? Math.Abs(Convert.ToDouble(dtUserRecords.Rows[i][24].ToString())) : float.Parse("0");
                                            clientcsvdataMst.ExchangeRate = dtUserRecords.Rows[i][25].ToString() != "" ? Math.Abs(Convert.ToDouble(dtUserRecords.Rows[i][25].ToString())) : float.Parse("0");
                                            clientcsvdataMst.MarketValueInRands = dtUserRecords.Rows[i][26].ToString() != "" ? Math.Abs(Convert.ToDouble(dtUserRecords.Rows[i][26].ToString())) : float.Parse("0");
                                            clientcsvdataMst.AnnuitRevisionEffectiveMonth = dtUserRecords.Rows[i][27].ToString() != "" ? dtUserRecords.Rows[i][27].ToString() : "";
                                            clientcsvdataMst.NetCapitalGainOrLoss = dtUserRecords.Rows[i][28].ToString() != "" ? dtUserRecords.Rows[i][28].ToString() : "";
                                            clientcsvdataMst.ModelPortFolioName = dtUserRecords.Rows[i][29].ToString() != " " ? dtUserRecords.Rows[i][29].ToString() : " ";
                                            clientcsvdataMst.DimFee = dtUserRecords.Rows[i][30].ToString() != "" ? float.Parse(dtUserRecords.Rows[i][30].ToString()) : float.Parse("0");
                                            clientcsvdataMst.RicFee = dtUserRecords.Rows[i][31].ToString() != "" ? float.Parse(dtUserRecords.Rows[i][31].ToString()) : float.Parse("0");
                                            clientcsvdataMst.GroupRaemployer = dtUserRecords.Rows[i][32].ToString() != " " ? dtUserRecords.Rows[i][32].ToString() : " ";

                                            clientcsvList.Add(clientcsvdataMst);
                                        }

                                    }
                                }
                                else
                                {
                                    response.Status = false;
                                    response.Message = "Invalid File Format....!!!";
                                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                }
                                if (clientcsvList.Count > 0)
                                {
                                    _dBContext.AddRange(clientcsvList);
                                    _dBContext.SaveChanges();
                                }
                                else
                                {
                                    ErrorCount++;
                                }
                            }
                            else
                            {
                                response.Message = "Document Data is Null....";
                                ErrorCount++;
                            }

                            if (ErrorCount == 0)
                            {
                                var list = _dBContext.ClientCsvdataMsts.Where(x => x.ClientCsvfieldId == csvfileUploadLogMst.Id).ToList();
                                transactionScope1.Complete();
                                response.Status = true;
                                response.Message = "Document Uploaded Successfully....!!!";
                                response.StatusCode = System.Net.HttpStatusCode.OK;

                            }
                            else
                            {
                                transactionScope1.Dispose();
                            }
                        }
                    }
                    else
                    {
                        response.Message = "Document Is not Uploaded.....!!!";
                    }
                }
                else
                {
                    response.Message = "Invalid File Format...!!!";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return response;

        }

        public CommonResponse GetAllClientCSVDocumentData(GetAllClientCSVDataReqDTO getAllClientCSVDataReqDTO)
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


            int number = getAllClientCSVDataReqDTO.PageNumber == 0 ? (pagecount) : getAllClientCSVDataReqDTO.PageNumber;
            int size = getAllClientCSVDataReqDTO.PageSize == 0 ? (pageSize) : getAllClientCSVDataReqDTO.PageSize;
            bool orderby = getAllClientCSVDataReqDTO.Orderby == true ? (order) : getAllClientCSVDataReqDTO.Orderby;

            try
            {

                var totalCount = _dBContext.ClientCsvdataMsts.Count();
                List<ClientCSVDocumentDetails> ClientCSVDocumnetDetails = new List<ClientCSVDocumentDetails>();
                if (orderby)
                {
                    ClientCSVDocumnetDetails = _dBContext.ClientCsvdataMsts.Select(x => new ClientCSVDocumentDetails
                    {
                        AdviserCode = x.AdviserCode,
                        ClientName = x.ClientName,
                        RegistrationNumber = x.RegistrationNumber,
                        ClientNumber = x.ClientNumber,
                        Product = x.Product,
                        AccountName = x.AccountName,
                        AccountGroups = x.AccountGroups,
                        AccountNumber = x.AccountNumber,
                        InceptionDate = x.InceptionDate,
                        FundManager = x.FundManager,
                        FundName = x.FundName,
                        FundCode = x.FundCode,
                        FundCurrency = x.FundCurrency,
                        InitialAdvisorFee = x.InitialAdvisorFee,
                        AnnualAdvisorFee = x.AnnualAdvisorFee,
                        Section14AdvisorFeeRenewal = x.Section14AdvisorFeeRenewal,
                        MonthlyDebitOrder = x.MonthlyDebitOrder,
                        AnnuityIncomeRegularWithdrawal = x.AnnuityIncomeRegularWithdrawal,
                        AnnuityIncomeRegularWithdrawalFrequency = x.AnnuityIncomeRegularWithdrawalFrequency,
                        AnnuitRevisionEffectiveMonth = x.AnnuitRevisionEffectiveMonth,
                        AnnuityIncomeAnniversary = x.AnnuityIncomeAnniversary,
                        AccountFundAllocation = x.AccountFundAllocation,
                        UnitPriceCents = x.UnitPriceCents,
                        Units = x.Units,
                        PriceDate = x.PriceDate,
                        MarketValueInFundCurrency = x.MarketValueInFundCurrency,
                        NetCapitalGainOrLoss = x.NetCapitalGainOrLoss,
                        ModelPortFolioName = x.ModelPortFolioName,
                        DimFee = x.DimFee,
                        RicFee = x.RicFee,
                        GroupRaemployer = x.GroupRaemployer
                    }).OrderBy(on => on.AdviserCode)
                        .Skip((number - 1) * size)
                        .Take(size)
                        .ToList();
                }

                else
                {
                    ClientCSVDocumnetDetails = _dBContext.ClientCsvdataMsts.Select(x => new ClientCSVDocumentDetails
                    {
                        AdviserCode = x.AdviserCode,
                        ClientName = x.ClientName,
                        RegistrationNumber = x.RegistrationNumber,
                        ClientNumber = x.ClientNumber,
                        Product = x.Product,
                        AccountName = x.AccountName,
                        AccountGroups = x.AccountGroups,
                        AccountNumber = x.AccountNumber,
                        InceptionDate = x.InceptionDate,
                        FundManager = x.FundManager,
                        FundName = x.FundName,
                        FundCode = x.FundCode,
                        FundCurrency = x.FundCurrency,
                        InitialAdvisorFee = x.InitialAdvisorFee,
                        AnnualAdvisorFee = x.AnnualAdvisorFee,
                        Section14AdvisorFeeRenewal = x.Section14AdvisorFeeRenewal,
                        MonthlyDebitOrder = x.MonthlyDebitOrder,
                        AnnuityIncomeRegularWithdrawal = x.AnnuityIncomeRegularWithdrawal,
                        AnnuityIncomeRegularWithdrawalFrequency = x.AnnuityIncomeRegularWithdrawalFrequency,
                        AnnuitRevisionEffectiveMonth = x.AnnuitRevisionEffectiveMonth,
                        AnnuityIncomeAnniversary = x.AnnuityIncomeAnniversary,
                        AccountFundAllocation = x.AccountFundAllocation,
                        UnitPriceCents = x.UnitPriceCents,
                        Units = x.Units,
                        PriceDate = x.PriceDate,
                        MarketValueInFundCurrency = x.MarketValueInFundCurrency,
                        NetCapitalGainOrLoss = x.NetCapitalGainOrLoss,
                        ModelPortFolioName = x.ModelPortFolioName,
                        DimFee = x.DimFee,
                        RicFee = x.RicFee,
                        GroupRaemployer = x.GroupRaemployer


                    }).OrderByDescending(on => on.AdviserCode)
                         .Skip((number - 1) * size)
                         .Take(size)
                         .ToList();
                }
                if (ClientCSVDocumnetDetails != null)
                {
                    GetAllClientCSVDocumnetResDTO getAllClientCSVDocumnetResDTO = new GetAllClientCSVDocumnetResDTO();
                    getAllClientCSVDocumnetResDTO.TotalCount = totalCount;
                    getAllClientCSVDocumnetResDTO.ClientCSVDocumentList = ClientCSVDocumnetDetails;
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getAllClientCSVDocumnetResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
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
