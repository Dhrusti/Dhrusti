using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using ExcelDataReader;
using Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using System.Net;
using System.Transactions;

namespace BusinessLayer
{
    public class UploadCSVDocumentBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonHelper _commonHelper;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _configaration;
        private IHostingEnvironment _hostingEnvironment { get; }

        public UploadCSVDocumentBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configaration, IHostingEnvironment hostingEnvironment)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configaration = configaration;
            _hostingEnvironment = hostingEnvironment;
        }

        //public CommonResponse GetAllCSVDocumentData(GetAllCSVDataReqDTO getAllCSVDataReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    var pageData = _configaration.GetSection("ByDefaultPagination:Page");
        //    string pages = pageData.Value.ToString();
        //    int pagecount = Int32.Parse(pages);

        //    var totalPage = _configaration.GetSection("ByDefaultPagination:PageSize");
        //    string Size = totalPage.Value.ToString();
        //    int pageSize = Int32.Parse(Size);

        //    var orderBy = _configaration.GetSection("ByDefaultPagination:OrderBy");
        //    string orders = orderBy.Value.ToString();
        //    bool order = Boolean.Parse(orders);


        //    int number = getAllCSVDataReqDTO.PageNumber == 0 ? (pagecount) : getAllCSVDataReqDTO.PageNumber;
        //    int size = getAllCSVDataReqDTO.PageSize == 0 ? (pageSize) : getAllCSVDataReqDTO.PageSize;
        //    bool orderby = getAllCSVDataReqDTO.Orderby == true ? (order) : getAllCSVDataReqDTO.Orderby;

        //    try
        //    {
        //        List<CSVDocumentDetails> CSVDocumnetList = new List<CSVDocumentDetails>();
        //        var totalCount = _dBContext.CsvdataMsts.Count();
        //        if (orderby)
        //        {
        //            CSVDocumnetList = _dBContext.CsvdataMsts.Select(x => new CSVDocumentDetails
        //            {
        //                AccountNo = x.AccountNo,
        //                Surname = x.Surname,
        //                Category = x.Category,
        //                InvDate = x.InvDate,
        //                Share = x.Share,
        //                Quantity = x.Quantity,
        //                Price = x.Price,
        //                Value = x.Value,
        //                PercentTot = x.PercentTot
        //            })
        //                //.OrderBy(on => on.AccountNo)
        //                .Skip((number - 1) * size)
        //                .Take(size)
        //                .ToList();
        //        }

        //        else
        //        {
        //            CSVDocumnetList = _dBContext.CsvdataMsts.Select(x => new CSVDocumentDetails
        //            {
        //                AccountNo = x.AccountNo,
        //                Surname = x.Surname,
        //                Category = x.Category,
        //                InvDate = x.InvDate,
        //                Share = x.Share,
        //                Quantity = x.Quantity,
        //                Price = x.Price,
        //                Value = x.Value,
        //                PercentTot = x.PercentTot

        //            })
        //                 //.OrderByDescending(on => on.AccountNo)
        //                 .Skip((number - 1) * size)
        //                 .Take(size)
        //                 .ToList();
        //        }
        //        if (CSVDocumnetList != null)
        //        {
        //            GetAllCSVDocumnetResDTO getAllCSVDocumnetResDTO = new GetAllCSVDocumnetResDTO();
        //            getAllCSVDocumnetResDTO.TotalCount = totalCount;
        //            getAllCSVDocumnetResDTO.CSVDocumentList = CSVDocumnetList;
        //            commonResponse.Message = "Success";
        //            commonResponse.Status = true;
        //            commonResponse.StatusCode = HttpStatusCode.OK;
        //            commonResponse.Data = getAllCSVDocumnetResDTO;
        //        }
        //        else
        //        {
        //            commonResponse.Message = "Data Not Found.";
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return commonResponse;
        //}

        public CommonResponse UploadPPMCSVDocument(UploadPPMCSVDocumentReqDTO uploadDocumentReqDTO)
        {
            CommonResponse response = new CommonResponse();
            List<CsvdataMst> csvlist = new List<CsvdataMst>();
            try
            {
                int ErrorCount = 0;
                DataSet dsExcelRecords = new DataSet();
                IExcelDataReader reader = null;
                CsvfileUploadLogMst csv = new CsvfileUploadLogMst();

                var files = uploadDocumentReqDTO.File;
                FileInfo fileInfo = new FileInfo(files.FileName);
                string csvFileName = files.FileName;
                string FileExtension = fileInfo.Extension.ToLower();
                long FileSize = files.Length;
                bool validateFileExtension = false;
                bool IsFileDuplicate = false;
                bool IsFileUploadLogDuplicate = false;
                string[] allowedFileExtensions = { ".csv" };
                validateFileExtension = allowedFileExtensions.Contains(FileExtension) ? true : false;
                var CSVDataLog = _dBContext.CsvfileUploadLogMsts.FirstOrDefault(x => x.CsvfileName == csvFileName);
                var CSVData = _dBContext.CsvdataMsts.AsQueryable();
                if (CSVDataLog != null)
                {
                    CSVData = CSVData.Where(x => x.CsvfileId == CSVDataLog.Id);
                }
                //var CSVData = _dBContext.CsvdataMsts.Where(x => x.CsvfileId == CSVDataLog.Id).ToList();
                if (CSVDataLog != null)
                {
                    IsFileUploadLogDuplicate = true;
                }

                if (CSVData.Count() > 0)
                {
                    IsFileDuplicate = true;

                }
                if (validateFileExtension)
                {

                    CsvfileUploadLogMst csvfileUploadLogMst = new CsvfileUploadLogMst();
                    var UploadFileresponse = _commonHelper.UploadFile(files, "CSVFile", files.FileName);
                    UploadCSVDataReqDTO uploadCSVDataReqDTO = new UploadCSVDataReqDTO();
                    if (UploadFileresponse.Status)
                    {
                        using (TransactionScope transactionScope1 = new TransactionScope())
                        {

                            if (!IsFileUploadLogDuplicate)
                            {
                                csvfileUploadLogMst.ServiceProviderId = 3; //Service Provider = PPM = 3
                                csvfileUploadLogMst.CsvfileName = files.FileName;
                                csvfileUploadLogMst.FileSize = Convert.ToString(files.Length);
                                csvfileUploadLogMst.Extension = FileExtension;
                                csvfileUploadLogMst.Path = "";
                                csvfileUploadLogMst.CreatedBy = uploadDocumentReqDTO.UserId;
                                csvfileUploadLogMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                _dBContext.CsvfileUploadLogMsts.Add(csvfileUploadLogMst);
                                _dBContext.SaveChanges();
                            }
                            else
                            {
                                csvfileUploadLogMst = CSVDataLog;
                                csvfileUploadLogMst.FileSize = Convert.ToString(files.Length);
                                csvfileUploadLogMst.CreatedBy = uploadDocumentReqDTO.UserId;
                                csvfileUploadLogMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                _dBContext.Entry(csvfileUploadLogMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                _dBContext.SaveChanges();
                            }


                            //if (IsFileUploadLogDuplicate)
                            //{
                            //    CsvfileUploadLogMst csvfileUploadLog = new CsvfileUploadLogMst();
                            //    csvfileUploadLog = CSVDataLog;
                            //    csvfileUploadLog.FileSize = Convert.ToString(files.Length);
                            //    csvfileUploadLog.CreatedBy = uploadDocumentReqDTO.UserId;
                            //    csvfileUploadLog.CreatedDate = _commonHelper.GetCurrentDateTime();

                            if (IsFileUploadLogDuplicate)
                            {
                                _dBContext.CsvdataMsts.RemoveRange(CSVData);
                                _dBContext.SaveChanges();

                            }

                            string FullPath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", UploadFileresponse.Data);

                            //var stream = fileInfo.Open(FileMode.Open);
                            var stream = System.IO.File.Open(FullPath, FileMode.Open, FileAccess.Read);
                            reader = ExcelReaderFactory.CreateCsvReader(stream);
                            dsExcelRecords = reader.AsDataSet();
                            reader.Close();

                            if ((dsExcelRecords != null))
                            {
                                DataTable dtUserRecords = dsExcelRecords.Tables[0];
                                if (dtUserRecords.Rows[0][0].ToString() == "AccountNo" &&
                                    dtUserRecords.Rows[0][1].ToString() == "Surname" &&
                                    dtUserRecords.Rows[0][2].ToString() == "Category" &&
                                    dtUserRecords.Rows[0][3].ToString() == "Date" &&
                                    dtUserRecords.Rows[0][4].ToString() == "Share" &&
                                    dtUserRecords.Rows[0][5].ToString() == "Quantity" &&
                                    dtUserRecords.Rows[0][6].ToString() == "Price" &&
                                    dtUserRecords.Rows[0][7].ToString() == "Value" &&
                                    dtUserRecords.Rows[0][8].ToString() == "PercentTot")
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
                                            (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][8]))))
                                        {


                                            int intvalue;
                                            DateTime invDate = new DateTime();
                                            double doublevalue;
                                            string invDate1 = Convert.ToString(dtUserRecords.Rows[i][3]);
                                            string Quantity = Convert.ToString(dtUserRecords.Rows[i][5]);
                                            string Price = Convert.ToString(dtUserRecords.Rows[i][6]);
                                            string? Value = Convert.ToString(dtUserRecords.Rows[i][7]);
                                            string PercentTot = Convert.ToString(dtUserRecords.Rows[i][8]);


                                            string[] formats = { "dd-MM-yy", "dd MMMM yyyy", "dd-MMM-yy", "dd-MM-yyyy", "dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "MM-dd-yyyy", "MM/dd/yyyy" };

                                            bool IsInvDateValid = !string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][3])) ? DateTime.TryParseExact(invDate1, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out invDate) : false;

                                            bool IsValue = !string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][7])) ? double.TryParse(Value, out doublevalue) : false;
                                            bool IsQuantity = !string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][5])) ? int.TryParse(Quantity, out intvalue) : false;
                                            bool IsPrice = !string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][6])) ? double.TryParse(Price, out doublevalue) : false;
                                            bool IsPercentTot = !string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][8])) ? double.TryParse(PercentTot, out doublevalue) : false;


                                            Boolean isInserrtvalue = false;
                                            if (string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][3])) || IsInvDateValid == true)
                                            {
                                                isInserrtvalue = true;
                                            }
                                            else
                                            {
                                                response.Status = false;
                                                response.Message = "Invalid File data";
                                                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                                ErrorCount++;
                                                break;
                                            }
                                            if (string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][5])) || IsQuantity == true)
                                            {
                                                isInserrtvalue = true;
                                            }
                                            else
                                            {
                                                response.Status = false;
                                                response.Message = "Invalid File data";
                                                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                                ErrorCount++;
                                                break;

                                            }

                                            if (string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][6])) || IsPrice == true)
                                            {
                                                isInserrtvalue = true;

                                            }
                                            else
                                            {
                                                response.Status = false;
                                                response.Message = "Invalid File data";
                                                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                                ErrorCount++;
                                                break;
                                            }

                                            if (string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][7])) || IsValue == true)
                                            {

                                                isInserrtvalue = true;
                                            }
                                            else
                                            {
                                                response.Status = false;
                                                response.Message = "Invalid File data";
                                                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                                ErrorCount++;
                                                break;
                                            }

                                            if (string.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][8])) || IsPercentTot == true)
                                            {
                                                isInserrtvalue = true;
                                            }
                                            else
                                            {
                                                response.Status = false;
                                                response.Message = "Invalid File data";
                                                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                                ErrorCount++;
                                                break;
                                            }

                                            if (isInserrtvalue = true)
                                            {

                                                csvDataMst.CsvfileId = csvfileUploadLogMst.Id;
                                                csvDataMst.AccountNo = dtUserRecords.Rows[i][0] != null ? Convert.ToString(dtUserRecords.Rows[i][0]) : "";
                                                csvDataMst.Surname = dtUserRecords.Rows[i][1].ToString() != " " ? Convert.ToString(dtUserRecords.Rows[i][1]) : " ";
                                                csvDataMst.Category = dtUserRecords.Rows[i][2].ToString() != " " ? Convert.ToString(dtUserRecords.Rows[i][2]) : " ";
                                                //csvDataMst.InvDate = (!String.IsNullOrWhiteSpace(Convert.ToString(dtUserRecords.Rows[i][3]))) ? (Convert.ToDateTime(Convert.ToString(dtUserRecords.Rows[i][3]))) : null;
                                                csvDataMst.InvDate = invDate;
                                                csvDataMst.Share = dtUserRecords.Rows[i][4].ToString() != "" ? Convert.ToString(dtUserRecords.Rows[i][4]) : " ";
                                                csvDataMst.Quantity = dtUserRecords.Rows[i][5].ToString() != "" ? Convert.ToInt32(dtUserRecords.Rows[i][5]) : 0;
                                                csvDataMst.Price = dtUserRecords.Rows[i][6].ToString() != "" ? Math.Abs(Convert.ToDouble(dtUserRecords.Rows[i][6].ToString())) : 0;
                                                csvDataMst.Value = dtUserRecords.Rows[i][7].ToString() != "" ? Math.Abs(Convert.ToDouble(dtUserRecords.Rows[i][7].ToString())) : 0;
                                                csvDataMst.PercentTot = dtUserRecords.Rows[i][8].ToString() != "" ? Math.Abs(Convert.ToDouble(dtUserRecords.Rows[i][8].ToString())) : 0;

                                                csvlist.Add(csvDataMst);

                                                uploadCSVDataReqDTO.AccountNo = csvDataMst.AccountNo;
                                                uploadCSVDataReqDTO.Surname = csvDataMst.Surname;
                                                uploadCSVDataReqDTO.Category = csvDataMst.Category;
                                                uploadCSVDataReqDTO.InvDate = csvDataMst.InvDate;
                                                uploadCSVDataReqDTO.Share = csvDataMst.Share;
                                                uploadCSVDataReqDTO.Quantity = csvDataMst.Quantity;
                                                uploadCSVDataReqDTO.Price = csvDataMst.Price;
                                                uploadCSVDataReqDTO.Value = csvDataMst.Value;
                                                uploadCSVDataReqDTO.PercentTot = csvDataMst.PercentTot;
                                            }
                                            else
                                            {
                                                response.Status = false;
                                                response.Message = "Invalid File data";
                                                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                                ErrorCount++;
                                                break;
                                            }

                                        }

                                    }
                                }
                                else
                                {
                                    response.Status = false;
                                    response.Message = "Invalid File Data";
                                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                    ErrorCount++;
                                }
                                if (csvlist.Count > 0)
                                {

                                    _dBContext.AddRange(csvlist);
                                    _dBContext.SaveChanges();
                                }
                                else
                                {
                                    response.Status = false;
                                    response.Message = "Invalid File Data";
                                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                                    ErrorCount++;
                                }
                            }
                            else
                            {
                                response.Message = "Invalid File Data";
                                ErrorCount++;
                            }
                            if (ErrorCount == 0)
                            {
                                var list = _dBContext.CsvdataMsts.Where(x => x.CsvfileId == csvfileUploadLogMst.Id).ToList();
                                transactionScope1.Complete();
                                response.Status = true;
                                response.Message = "Document Uploaded Successfully";
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
                        response.Message = "Invalid File Format";
                    }
                }
                else
                {
                    response.Message = "Invalid File!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        //Read Data From FTP Server
        //public CommonResponse UploadCSVDocument(UploadCSVDocumentReqDTO uploadDocumentReqDTO)
        //{
        //    CommonResponse response = new CommonResponse();
        //    StringBuilder result = new StringBuilder();
        //    FtpWebRequest reqFTP;
        //    try
        //    {
        //        String ftpserver = _configaration.GetSection("FTPCredetial:FTPServer").Value;

        //        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpserver));
        //        reqFTP.UsePassive = Convert.ToBoolean(_configaration.GetSection("FTPCredetial:UsePassive").Value);
        //        reqFTP.UseBinary = Convert.ToBoolean(_configaration.GetSection("FTPCredetial:UseBinary").Value);
        //        string UserName = _configaration.GetSection("FTPCredetial:UserName").Value;
        //        string Password = _configaration.GetSection("FTPCredetial:Password").Value;
        //        reqFTP.Credentials = new NetworkCredential(UserName, Password);
        //        reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
        //        reqFTP.Proxy = GlobalProxySelection.GetEmptyWebProxy();
        //        FtpWebResponse response1 = (FtpWebResponse)reqFTP.GetResponse();

        //        StreamReader reader = new StreamReader(response1.GetResponseStream(), System.Text.Encoding.UTF8);

        //        string line = "";
        //        //Read CSV File from the FTP Server.
        //        while (reader.Peek() > -1)
        //        {
        //            line = reader.ReadLine();
        //        }

        //        if (result.ToString().LastIndexOf('\n') >= 0)
        //            result.Remove(result.ToString().LastIndexOf('\n'), 1);
        //        reader.Close();
        //        response1.Close();

        //        response.StatusCode = HttpStatusCode.OK;
        //        response.Data = result.ToString();
        //        response.Message = "DOcument Uploaded Successfully...!!!";
        //        // return result.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Exception...!!!";
        //    }
        //    return response;
        //}

        //public CommonResponse Download(string filePath, string fileName, string URL)
        //{
        //    CommonResponse response = new CommonResponse();
        //    FtpWebRequest reqFTP;
        //    try
        //    {
        //        string UserName = _configaration.GetSection("FTPCredetial:UserName").Value;
        //        string Password = _configaration.GetSection("FTPCredetial:Password").Value;

        //        //filePath = <<The full path where the file is to be created.>>, 
        //        //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
        //        FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

        //        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(URL + fileName));
        //        reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

        //        reqFTP.UseBinary = true;
        //        reqFTP.Credentials = new NetworkCredential(UserName, Password);
        //        reqFTP.Timeout = 500000;
        //        FtpWebResponse response1 = (FtpWebResponse)reqFTP.GetResponse();
        //        Stream ftpStream = response1.GetResponseStream();
        //        long cl = response1.ContentLength;
        //        int bufferSize = 2048;
        //        int readCount;
        //        byte[] buffer = new byte[bufferSize];
        //        readCount = ftpStream.Read(buffer, 0, bufferSize);
        //        while (readCount > 0)
        //        {
        //            outputStream.Write(buffer, 0, readCount);
        //            readCount = ftpStream.Read(buffer, 0, bufferSize);
        //        }

        //        ftpStream.Close();
        //        outputStream.Close();
        //        response1.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return response;
        //}

        //150922-SP //Download File from FTP Server
        //public CommonResponse UploadCSVDocument(UploadCSVDocumentReqDTO uploadDocumentReqDTO)
        //{

        //    CommonResponse commonResponse = new CommonResponse();

        //    string fileName = "PPM20220711-New.csv";
        //    string ftp = _configaration.GetSection("FTPCredetial:FTPServer").Value;
        //    string UserName = _configaration.GetSection("FTPCredetial:UserName").Value;
        //    string Password = _configaration.GetSection("FTPCredetial:Password").Value;
        //    try
        //    {
        //        int ErrorCount = 0;
        //        DataSet dsExcelRecords = new DataSet();
        //        IExcelDataReader reader = null;

        //        // Stream requeststream = null;
        //        FileStream localFileStream = null;
        //        FtpWebRequest request1 = (FtpWebRequest)WebRequest.Create(_configaration.GetSection("FTPCredetial:FTPServer").Value);
        //        request1.Credentials = new NetworkCredential(UserName, Password);
        //        request1.Method = WebRequestMethods.Ftp.UploadFile;
        //        //var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "CSVFile");
        //        using (Stream ftpStream = request1.GetResponse().GetResponseStream())
        //        {
        //            using (Stream fileStream = File.Create(@"D:\WCM-BE\WCM-BE\WaltCapitalManagement\WaltCapitalManagementWebAPI\wwwroot\Files\CSVFile\PPM20220711-New.csv"))
        //            {
        //                {

        //                    ftpStream.CopyTo(fileStream);

        //                    int buffersize = 524288;

        //                    byte[] buffer = new byte[buffersize];
        //                    localFileStream = File.OpenRead(@"D:\WCM-BE\WCM-BE\WaltCapitalManagement\WaltCapitalManagementWebAPI\wwwroot\Files\CSVFile\PPM20220711-New.csv");


        //                    int readcount = localFileStream.Read(buffer, 0, buffersize);
        //                    long bytessentcounter = 0;



        //                    while (readcount > 0)
        //                    {
        //                        //requeststream.Write(buffer, 0, readcount);
        //                        bytessentcounter += readcount;
        //                        readcount = localFileStream.Read(buffer, 0, buffersize);

        //                        commonResponse.Message = "Success...";
        //                        commonResponse.StatusCode = HttpStatusCode.OK;
        //                        commonResponse.Status = true;
        //                        //System.threading.thread.sleep(100);
        //                    }


        //                    fileStream.Close();
        //                    localFileStream.Close();

        //                    //ftpstream.close();

        //                }
        //            }
        //        }

        //    }
        //    catch (WebException ex)
        //    {
        //        throw;
        //    }
        //    return commonResponse;
        //}

        //public CommonResponse UploadCSVDocument(UploadCSVDocumentReqDTO uploadDocumentReqDTO)
        //{
        //    string ftpURL = _configaration.GetSection("FTPCredetial:FTPServer").Value;
        //    string UserName = _configaration.GetSection("FTPCredetial:UserName").Value;
        //    string Password = _configaration.GetSection("FTPCredetial:Password").Value;
        //    //  string ftpDirectory = ;
        //    string FileName = "PPM20220711-New.csv";
        //    string LocalDirectory = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files");


        //    CommonResponse commonResponse = new CommonResponse();
        //    //if (!File.Exists(LocalDirectory + "/" + FileName))
        //    //{
        //    try
        //    {
        //        FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(ftpURL + "/" + FileName);
        //        requestFileDownload.Credentials = new NetworkCredential(UserName, Password);
        //        requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
        //        FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
        //        Stream responseStream = responseFileDownload.GetResponseStream();
        //        FileStream writeStream = new FileStream(LocalDirectory + "/" + FileName, FileMode.Create);
        //        int Length = 33586;
        //        Byte[] buffer = new Byte[Length];
        //        //Stream fileStream = new FileStream(path + "/" + file, FileMode.OpenOrCreate
        //        int bytesRead = responseStream.Read(buffer, 0, Length);
        //        responseStream.CopyTo(writeStream);
        //        responseStream.Close();
        //        while (bytesRead > 0)
        //        {
        //            writeStream.Write(buffer, 0, bytesRead);
        //            bytesRead = responseStream.Read(buffer, 0, Length);
        //        }
        //        responseStream.Close();
        //        writeStream.Close();
        //        commonResponse.Message = "Success......";
        //        commonResponse.StatusCode = HttpStatusCode.OK;
        //        commonResponse.Status = true;
        //        requestFileDownload = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    // }
        //    return commonResponse;
        //}

        public CommonResponse UploadCSVData(UploadCSVDataDocumentReqDTO uploadCSVDataDocumentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (uploadCSVDataDocumentReqDTO.ServiceProviderId > 0)
                {
                    if (uploadCSVDataDocumentReqDTO.ServiceProviderId == 1) // serviceProvideerId = 1 = PPM
                    {

                    }
                    else if (uploadCSVDataDocumentReqDTO.ServiceProviderId == 2) // serviceProvideerId = 2 = PPM
                    {

                    }
                    else if (uploadCSVDataDocumentReqDTO.ServiceProviderId == 3) // serviceProvideerId = 3 = PPM
                    {
                        UploadPPMCSVDocumentReqDTO uploadPPMCSVDocumentReqDTO = new UploadPPMCSVDocumentReqDTO();
                        uploadPPMCSVDocumentReqDTO.UserId = uploadCSVDataDocumentReqDTO.UserId.Value;
                        uploadPPMCSVDocumentReqDTO.File = uploadCSVDataDocumentReqDTO.File;
                        commonResponse = UploadPPMCSVDocument(uploadPPMCSVDocumentReqDTO);
                    }
                    else if (uploadCSVDataDocumentReqDTO.ServiceProviderId == 4) // serviceProvideerId = 4 = PPM
                    {

                    }
                    else if (uploadCSVDataDocumentReqDTO.ServiceProviderId == 5) // serviceProvideerId = 5 = PPM
                    {

                    }
                }
                else
                {
                    commonResponse.Message = "Please Enter Valid ServiceProviderId!";
                    commonResponse.Status = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllCSVDocumentData(GetAllCSVDataDocumentReqDTO getAllCSVDataDocumentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            //var pageData = _configaration.GetSection("ByDefaultPagination:Page");
            //string pages = pageData.Value.ToString();
            //int pagecount = Int32.Parse(pages);

            //var totalPage = _configaration.GetSection("ByDefaultPagination:PageSize");
            //string Size = totalPage.Value.ToString();
            //int pageSize = Int32.Parse(Size);

            //var orderBy = _configaration.GetSection("ByDefaultPagination:OrderBy");
            //string orders = orderBy.Value.ToString();
            //bool order = Boolean.Parse(orders);

            int Page = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:Page").Value);
            int PageSize = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:PageSize").Value);
            bool? OrderBy = Convert.ToBoolean(_configaration.GetSection("ByDefaultPagination:OrderBy").Value);

            int number = getAllCSVDataDocumentReqDTO.PageNumber == 0 ? (Page) : getAllCSVDataDocumentReqDTO.PageNumber;
            int size = getAllCSVDataDocumentReqDTO.PageSize == 0 ? (PageSize) : getAllCSVDataDocumentReqDTO.PageSize;
            bool? orderby = getAllCSVDataDocumentReqDTO.Orderby == true ? (OrderBy) : getAllCSVDataDocumentReqDTO.Orderby;

            try
            {
                GetAllCSVDataDocumentResDTO getAllCSVDocumnetResDTO = new GetAllCSVDataDocumentResDTO();
                List<CSVDataDocumentDetails> CSVDocumnetList = new List<CSVDataDocumentDetails>();
                //var totalCount = _dBContext.CsvfileUploadLogMsts.Count();
                var CsvfileUploadLogMstsLists = _commonRepo.getPPMCSVLogList();
                if (getAllCSVDataDocumentReqDTO.ServiceProviderId > 0)
                {
                    CsvfileUploadLogMstsLists = CsvfileUploadLogMstsLists.Where(x => x.ServiceProviderId == getAllCSVDataDocumentReqDTO.ServiceProviderId);
                }
                if (getAllCSVDataDocumentReqDTO.CreatedDate != null)
                {
                    CsvfileUploadLogMstsLists = CsvfileUploadLogMstsLists.Where(x => x.CreatedDate.Date == getAllCSVDataDocumentReqDTO.CreatedDate.Value);
                }

                CsvfileUploadLogMstsLists.ToList();

                CSVDocumnetList = (from cFU in CsvfileUploadLogMstsLists
                                   join client in _commonRepo.getUserList() on cFU.CreatedBy equals client.Id
                                   into clientTemp
                                   from clientFinal in clientTemp.DefaultIfEmpty()
                                   join SP in _commonRepo.serviceProviderList() on cFU.ServiceProviderId equals SP.Id into SPUlTemp
                                   from SPFinal in SPUlTemp.DefaultIfEmpty()
                                   select new CSVDataDocumentDetails
                                   {
                                       Id = cFU.Id,
                                       ServiceProviderName = SPFinal.ServiceProvider,
                                       UploadDate = cFU.CreatedDate.Date,
                                       FileName = cFU.CsvfileName,
                                       UploadedBy = clientFinal.FirstName + " " + clientFinal.LastName,
                                   }).ToList();
               
                if (!string.IsNullOrWhiteSpace(getAllCSVDataDocumentReqDTO.SearchString))
                {
                    CSVDocumnetList = CSVDocumnetList.Where(x => x.FileName.ToLower().Contains(getAllCSVDataDocumentReqDTO.SearchString.ToLower())).ToList();
                  
                }
                getAllCSVDocumnetResDTO.TotalCount = CSVDocumnetList.Count();
                if (orderby.Value)
                {
                    CSVDocumnetList = CSVDocumnetList.OrderBy(x => x.UploadDate).ThenBy(x => x.Id).Skip((number - 1) * size)
                                       .Take(size)
                                       .ToList();
                }
                else
                {
                    CSVDocumnetList = CSVDocumnetList.OrderByDescending(x => x.UploadDate).ThenByDescending(x => x.Id).Skip((number - 1) * size)
                                     .Take(size)
                                     .ToList();
                }

                if (CSVDocumnetList.Count > 0)
                {

                    getAllCSVDocumnetResDTO.CSVDataDocumentList = CSVDocumnetList;
                   
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getAllCSVDocumnetResDTO;
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
