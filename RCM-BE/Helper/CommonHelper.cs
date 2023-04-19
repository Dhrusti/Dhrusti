using Helper.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Helper
{
    public class CommonHelper
    {
        public const string DATE_FORMAT = "dd/MM/yyyy";
        private IConfiguration _configuration { get; }
        private IHostingEnvironment _hostingEnvironment { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonHelper(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public static DataTable ModelListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static DataTable ModelToDataTable<T>(T items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }

            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                values[i] = Props[i].GetValue(items, null);
            }
            dataTable.Rows.Add(values);

            return dataTable;
        }

        public List<dynamic> DataTableToDynamicList<T>(DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
            }
            return dynamicDt;
        }

        public static string GetDateInString(DateTime date)
        {
            return date.ToString(DATE_FORMAT).Replace("-", "/");
        }

        public DateTime GetDateFromString(string date)
        {
            return DateTime.ParseExact(date, DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        public CommonResponse ExecuteCRUDStoreProcedure(string queryText, SqlParameter[] sqlParameters, bool jsonInput = false)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                string connectionString = _configuration.GetConnectionString("EntitiesConnection");
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = queryText;

                    if (jsonInput)
                    {
                        sqlCommand.Parameters.AddWithValue("@InputParam", JsonConvert.SerializeObject(sqlParameters).ToString());
                    }
                    else
                    {
                        if (sqlParameters != null)
                        {
                            foreach (SqlParameter para in sqlParameters)
                            {
                                if (para != null)
                                    sqlCommand.Parameters.Add(para);
                            }
                        }
                    }

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlCommand))
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count != 0)
                            response.Data = DataTableToDynamicList<dynamic>(ds.Tables[0]);
                        response.Status = true;
                        response.Message = "Success.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public SqlParameter[] GenerateAdvanceSqlParameters(dynamic model, int spType, int PageNumber = 1, int RowsOfPage = 10)
        {
            Type ApplicantInfo = model.GetType();
            PropertyInfo[] properties = ApplicantInfo.GetProperties();
            var TotalParameters = properties.Count();
            if (spType == CommonConstant.Delete)
                TotalParameters = 1;
            if (spType == CommonConstant.ReadWithFilter)
                TotalParameters = TotalParameters + 2;
            SqlParameter[] sqlParameters = new SqlParameter[TotalParameters + 1];

            int i = 0;

            sqlParameters[i] = new SqlParameter();
            sqlParameters[i].ParameterName = "@CRUDInput";
            // sqlParameters[i].SqlDbType = SqlDbType.Int;
            sqlParameters[i].Value = spType;
            if (spType == CommonConstant.ReadWithFilter)
            {
                sqlParameters[i] = new SqlParameter();
                sqlParameters[i].ParameterName = "@PageNumber";
                //sqlParameters[i].SqlDbType = SqlDbType.Int;
                sqlParameters[i].Value = PageNumber;

                sqlParameters[i] = new SqlParameter();
                sqlParameters[i].ParameterName = "@RowsOfPage";
                // sqlParameters[i].SqlDbType = SqlDbType.Int;
                sqlParameters[i].Value = RowsOfPage;
            }

            if (spType != CommonConstant.Delete)
            {
                foreach (PropertyInfo property in properties)
                {
                    i++;
                    sqlParameters[i] = new SqlParameter();
                    sqlParameters[i].ParameterName = "@" + property.Name.ToString();
                    //sqlParameters[i].SqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), property.PropertyType.ToString());
                    sqlParameters[i].Value = model.GetType().GetProperty(property.Name).GetValue(model, null);
                }
            }
            else
            {
                i++;
                sqlParameters[i] = new SqlParameter();
                sqlParameters[i].ParameterName = "@Id";
                //sqlParameters[i].SqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), property.PropertyType.ToString());
                sqlParameters[i].Value = model;

            }

            return sqlParameters;
        }

        public SqlParameter[] GenerateSqlParameters(dynamic model)
        {
            Type ApplicantInfo = model.GetType();
            PropertyInfo[] properties = ApplicantInfo.GetProperties();
            var TotalParameters = properties.Count();
            SqlParameter[] sqlParameters = new SqlParameter[TotalParameters];

            int i = 0;
            foreach (PropertyInfo property in properties)
            {

                sqlParameters[i] = new SqlParameter();
                sqlParameters[i].ParameterName = "@" + property.Name.ToString();
                //sqlParameters[i].SqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), property.PropertyType.ToString());
                sqlParameters[i].Value = model.GetType().GetProperty(property.Name).GetValue(model, null);
                i++;
            }
            return sqlParameters;
        }

        public CommonResponse UploadFile(IFormFile file, string subDirectory, string fileName)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                string savePath = string.Empty;
                string CurrentDirectory = Directory.GetCurrentDirectory();
                subDirectory = subDirectory ?? string.Empty;
                var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files", subDirectory);


                Directory.CreateDirectory(target);
                savePath = Path.Combine("Files", subDirectory, fileName);
                var filePath = Path.Combine(target, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                response.Status = true;
                response.Message = "File Uploaded";
                response.Data = savePath;
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Message = ex.Message;
            }
            return response;
        }

        public CommonResponse UploadFile(string file, string subDirectory, string fileName)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                string savePath = string.Empty;
                string CurrentDirectory = Directory.GetCurrentDirectory();
                subDirectory = subDirectory ?? string.Empty;
                var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files", subDirectory);

                Directory.CreateDirectory(target);
                savePath = Path.Combine("Files", subDirectory, fileName);
                var filePath = Path.Combine(target, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //file.CopyTo(stream);
                    Byte[] byteArray = System.Convert.FromBase64String(file.Split(',')[1]);
                    stream.Write(byteArray, 0, byteArray.Length);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Status = true;
                response.Message = "File Uploaded";
                response.Data = savePath;
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Message = ex.Message;
            }
            return response;
        }

        public string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }

        public string GenerateRandomOTP()
        {
            int iOTPLength = 6;
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        public CommonResponse SendEmail(SendEmailRequestModel model)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(model.ToEmail))
                {
                    MailMessage mail = new MailMessage();

                    //get configration from appsettings.json
                    string FromEmail = _configuration.GetSection("SiteEmailConfigration:FromEmail").Value;
                    string Host = _configuration.GetSection("SiteEmailConfigration:Host").Value;
                    int Port = Convert.ToInt32(_configuration.GetSection("SiteEmailConfigration:Port").Value);
                    bool EnableSSL = Convert.ToBoolean(_configuration.GetSection("SiteEmailConfigration:EnableSSL").Value);
                    string Password = _configuration.GetSection("SiteEmailConfigration:MailPassword").Value;
                    bool EmailEnable = Convert.ToBoolean(_configuration.GetSection("SiteEmailConfigration:EmailEnable").Value);
                    if (EmailEnable)
                    {
                        mail.From = new MailAddress(FromEmail, "Walt Capital Management");
                        mail.To.Add(new MailAddress(model.ToEmail));
                        mail.Subject = model.Subject;
                        mail.Body = model.Body;
                        mail.IsBodyHtml = true;
                        //if (model.Attachment != null)
                        //{

                        //    string path = Convert.ToString(model.Attachment);
                        //    Attachment attachment = new Attachment(path);
                        //    mail.Attachments.Add(attachment);
                        //}

                        if (model.Attachment != null)
                        {
                            mail.Attachments.Add(new Attachment(model.Attachment));
                        }

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = Host;
                        smtp.Port = Port;
                        smtp.EnableSsl = EnableSSL;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(FromEmail, Password);
                        try
                        {
                            smtp.Send(mail);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    response.Status = true;
                    response.Message = "Success.";
                }
                else
                {
                    response.Message = "Receiver Email Id Not Provided.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public void AddLog(string text, string? logType = null)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                logType = logType != null ? logType : "";
                string logFileName = GetCurrentDateTime().ToString("dd/MM/yyyy").Replace('/', '_').ToString() + ".log";
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Logs", logType);
                var exists = Directory.Exists(filePath);
                if (!exists)
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, logFileName);
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    text = GetCurrentDateTime().ToString() + " : " + text;
                    writer.WriteLine(string.Format(text, GetCurrentDateTime().ToString("dd/MM/yyyy hh:mm:ss tt")));
                    writer.Close();
                }
            }
        }

        public string CreateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public string EncryptString(string plainText)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["EncryptionSecurityKey"].ToString());
            var iv = Encoding.UTF8.GetBytes(_configuration["EncryptionSecurityIV"].ToString());
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.

            return Convert.ToBase64String(encrypted);
            //return encrypted;
        }

        public string DecryptString(string cipherText)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["EncryptionKeys:EncryptionSecurityKey"].ToString());
            var iv = Encoding.UTF8.GetBytes(_configuration["EncryptionKeys:EncryptionSecurityIV"].ToString());
            var encrypted = Convert.FromBase64String(cipherText);
            // Check arguments.
            if (encrypted == null || encrypted.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(encrypted))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }
            return plaintext;
        }

        public bool ValidateEmail(String email)
        {
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        public string GenerateIdByDigit(int digit, int Id)
        {
            string generatedId = "0";
            if (digit > 0 && Id > 0)
            {
                int digitCount = CountDigit(Id);
                if (digitCount > 0)
                {
                    //digit = digit - digitCount;
                    string digitvalue = "{0:D" + digit + "}";
                    generatedId = string.Format(digitvalue, Id);
                }
            }
            return generatedId;
        }

        private int CountDigit(int number)
        {
            int count = 0;
            while (number > 0)
            {
                number = number / 10;
                count++;
            }
            return count;
        }

        public string GetPhysicalRootPath()
        {
            string directoryPath = "/files";
            var physicalRootPath = _hostingEnvironment.WebRootPath + directoryPath;
            return physicalRootPath;
        }

        public string GetRelativeRootPath()
        {
            string directoryPath = "/files";
            string relativeRootPath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + directoryPath;
            return relativeRootPath;
        }

        public FilePaths GetFilePaths(string ModuleName, string extension, bool isTempFolder, string? fileName = "")
        {
            FilePaths filePaths = new FilePaths();
            var rootPath = _hostingEnvironment.WebRootPath;
            string directoryPath = "/files";
            if (isTempFolder)
            {
                directoryPath += "/temp";
            }
            string fileNameOnly = "_" + System.Guid.NewGuid() + "." + extension.ToString();
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                fileNameOnly = fileName + "." + extension.ToString();
            }

            directoryPath += "/" + ModuleName + "/" + System.DateTime.Now.Year + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Day + "/";
            if (!System.IO.Directory.Exists(rootPath + directoryPath))
            {
                System.IO.Directory.CreateDirectory(rootPath + directoryPath);
            }

            var fileNameFull = directoryPath + fileNameOnly;

            filePaths.FilePhysicalPath = rootPath + fileNameFull;
            filePaths.FileRelativePath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + fileNameFull;
            filePaths.DirectoryPhysicalPath = rootPath + directoryPath;
            filePaths.DirectoryRelativePath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + directoryPath;
            return filePaths;
        }

        public string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();

            mimeType = "data:" + mimeType + ";base64,";
            return mimeType;
        }
        public bool SaveImage(string ImgStr, string ImgName)
        {
            //String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path

            ////Check if directory exist
            //if (!System.IO.Directory.Exists(path))
            //{
            //    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            //}

            //string imageName = ImgName + ".jpg";

            ////set the image path
            //string imgPath = Path.Combine(path, imageName);

            //byte[] imageBytes = Convert.FromBase64String(ImgStr);

            //File.WriteAllBytes(imgPath, imageBytes);

            return true;
        }

        public string GetAge(DateTime dateTime)
        {
            DateTime birthDate = Convert.ToDateTime(dateTime);
            int age = (int)Math.Floor((GetCurrentDateTime() - birthDate).TotalDays / 365.25D);
            return age.ToString();
        }

        public DateTime StringToDate(string date)
        {
            string iString = date.ToString();
            string convertedDate = Convert.ToDateTime(iString).ToString("yyyy-MM-dd");
            DateTime oDate = DateTime.ParseExact(convertedDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            return oDate;
        }

        public string GetFormatedInt(int value)
        {
            string formatedInt = Convert.ToString(value);
            if (value > 0)
            {
                formatedInt = value.ToString("#,##0");
            }
            return formatedInt;
        }

        public string GetFormatedDecimal(decimal value)
        {
            string formatedDecimal = Convert.ToString(value);
            formatedDecimal = value > 0 ? Convert.ToString(TruncateDecimal(value, 2)) : value.ToString("#,##0.00");
            return formatedDecimal;
        }

        public string GetBase64FromFile(string filePath, string fileName)
        {
            string base64 = filePath;
            try
            {
                WebClient webClient = new WebClient();
                var fileByteArray = webClient.DownloadData(filePath);
                base64 = Convert.ToBase64String(fileByteArray);

                string mimeType = GetMimeType(fileName);
                base64 = mimeType + base64;
            }
            catch (Exception) { }
            return base64;
        }

        public string GetFormatedDouble(double value)
        {
            string formatedDouble = Convert.ToString(value);
            if (value > 0)
            {
                formatedDouble = value.ToString("#,##0.00");
            }
            return formatedDouble;
        }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public DateTime GetLastDateOfMonth(int Year, int Month)
        {
            DateTime LastDateOfMonth = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            return LastDateOfMonth;
        }

        public DateTime GetStartDateOfMonth(int Year, int Month)
        {
            DateTime StartDateOfMonth = new DateTime(Year, Month, 1);
            return StartDateOfMonth;
        }

        public decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            decimal tmp = Math.Truncate(step * value);
            return tmp / step;
        }

        public string GetUpperCaseFirstAll(string value)
        {
            return value != null ? Regex.Replace(value, @"(^\w)|(\s\w)", m => m.Value.ToUpper()) : string.Empty;
        }

        public string GetFormattedKeyName(string key)
        {
            if (key != null)
            {
                key = GetUpperCaseFirstAll(key);
                key = key.Substring(0, 1).ToLower() + key.Substring(1);
                key = key.Replace(" ", string.Empty);
            }
            else { key = string.Empty; }

            return key;
        }

        public async Task AddActivityLog(string apiUrl, string methodType, string request, string requestResult)
        {
            try
            {
                string logText = apiUrl + " (" + methodType + ") - Request : ( " + requestResult + " ) - Response : ( " + request + " ).";
                AddLog(logText, CommonConstant.Activity_log);
            }
            catch (Exception) { }
        }

        public void AddExceptionLog(string exceptionText)
        {
            try
            {
                AddLog(exceptionText, CommonConstant.Exception_log);
            }
            catch (Exception) { }
        }

        public string GetFormatedDecimalWithRounfOff(decimal value)
        {
            string formatedDecimal = Convert.ToString(value);
            formatedDecimal = value > 0 ? value.ToString("0.00") : value.ToString("0.00");
            return formatedDecimal;
        }

        //public dynamic ReadJsonFile(string FilePath)
        //{
        //    string FileText = File.ReadAllText(FilePath);
        //    dynamic model = JObject.Parse(FileText);
        //    //var model = JsonConvert.DeserializeObject<dynamic>(FileText);
        //    return model;
        //}

        public string ReadJsonFile(string FilePath)
        {
            string FileText = File.ReadAllText(FilePath);
            //dynamic model = JObject.Parse(FileText);
            //var model = JsonConvert.DeserializeObject<dynamic>(FileText);
            return FileText;
        }

        public bool AddJsonData(dynamic model, string FilePath)
        {
            var jsonString = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(FilePath, jsonString);
            return true;
        }
    }
}