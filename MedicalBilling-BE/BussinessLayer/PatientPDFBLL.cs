using DataLayer.Entities;
using DTO.ReqDTO;
using Helper;
using Helper.Models;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using DocumentITextPDF = iTextSharp.text.Document;
using FontITP = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace BussinessLayer
{
    public class PatientPDFBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;
        private readonly ClientBLL _clientBLL;
        private readonly DoctorBLL _doctorBLL;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PatientPDFBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper, ClientBLL clientBLL, DoctorBLL doctorBLL, IHostingEnvironment hostingEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;
            _clientBLL = clientBLL;
            _doctorBLL = doctorBLL;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public CommonResponse GeneratePatientPDF(GeneratePatientPDFReqDTO generatePatientPDFReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (generatePatientPDFReqDTO.PatientId != null)
                {

                    var appointmentDetails = _dbContext.AppointmentMsts.FirstOrDefault(x => x.Id == generatePatientPDFReqDTO.PatientId);
                    if (appointmentDetails != null)
                    {
                        string filePath = string.Empty;
                        string pdfFileName = string.Empty;
                        int width = 4500;
                        int tempHeight = 10200;
                        iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(width, tempHeight);
                        DocumentITextPDF document = new DocumentITextPDF();
                        document.SetMargins(15, 45, 90, 12);


                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {

                            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                            document.Open();

                            PdfContentByte content = writer.DirectContentUnder;
                            //var PDF = PdfWriter.GetInstance(document, new FileStream((filename), FileMode.Create));

                            FontITP fontSubHeaderBlue = FontFactory.GetFont("Arial", 10, FontITP.BOLD, WebColors.GetRGBColor("#0b6aab"));
                            FontITP fontSubHeaderBlueNormal = FontFactory.GetFont("Arial", 9, FontITP.NORMAL, WebColors.GetRGBColor("#0b6aab"));

                            FontITP fontBody = FontFactory.GetFont("Arial", 8, FontITP.NORMAL, BaseColor.BLACK);
                            FontITP fontBodyRed = FontFactory.GetFont("Arial", 8, FontITP.NORMAL, BaseColor.RED);
                            FontITP fontBodyBold = FontFactory.GetFont("Arial", 10, FontITP.BOLD, BaseColor.BLACK);

                            FontITP fontSummary = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 24, FontITP.NORMAL, BaseColor.BLACK);
                            FontITP header = new FontITP(FontITP.FontFamily.TIMES_ROMAN, 10f, FontITP.BOLD, BaseColor.BLACK);
                            FontITP cellValue = new FontITP(FontITP.FontFamily.TIMES_ROMAN, 10f, FontITP.NORMAL, BaseColor.BLACK);
                            //header.SetStyle(FontITP.UNDERLINE);
                            FontITP fontCHWTitle = FontFactory.GetFont("Arial", 10, FontITP.NORMAL, BaseColor.BLACK);
                            FontITP fontSubHeader = FontFactory.GetFont("Arial", 10, FontITP.NORMAL, BaseColor.BLACK);
                            var fontTableHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontITP.NORMAL, BaseColor.BLACK);
                            var fontTableRow = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, FontITP.NORMAL, BaseColor.GRAY);

                            PdfPTable acctable = new PdfPTable(8);
                            acctable.SpacingBefore = 100;
                            acctable.WidthPercentage = 100;
                            acctable.HorizontalAlignment = Rectangle.ALIGN_LEFT;




                            PdfPCell accHeader = new PdfPCell(new Phrase("Acc #", fontBodyBold));
                            accHeader.MinimumHeight = 20;
                            accHeader.Colspan = 6;
                            accHeader.Border = Rectangle.NO_BORDER;
                            accHeader.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                            acctable.AddCell(accHeader);

                            string? AccountNo = appointmentDetails.AccountNo != null ? Convert.ToString(appointmentDetails.AccountNo) : "";

                            PdfPCell accHeaderData = new PdfPCell(new Phrase(AccountNo, cellValue));
                            accHeader.MinimumHeight = 20;
                            accHeaderData.Colspan = 2;
                            accHeaderData.Border = Rectangle.BOX;
                            accHeaderData.BorderColor = BaseColor.BLACK;
                            accHeaderData.BorderWidth = 1;
                            accHeaderData.DisableBorderSide(Rectangle.TOP_BORDER);
                            accHeaderData.DisableBorderSide(Rectangle.LEFT_BORDER);
                            accHeaderData.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            //accHeaderData.Border = Rectangle.NO_BORDER;
                            accHeaderData.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            acctable.AddCell(accHeaderData);



                            PdfPCell dateHeader = new PdfPCell(new Phrase("Date of Appt.", fontBodyBold));
                            dateHeader.MinimumHeight = 20;
                            dateHeader.Colspan = 6;
                            dateHeader.Border = Rectangle.NO_BORDER;
                            dateHeader.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                            acctable.AddCell(dateHeader);

                            string? DateofAppt = appointmentDetails.ActualAppoitmentDate != null ? appointmentDetails.ActualAppoitmentDate.Value.ToString("MM-dd-yyyy") : null;
                            PdfPCell dateHeaderData = new PdfPCell(new Phrase(DateofAppt, cellValue));
                            dateHeaderData.MinimumHeight = 20;
                            dateHeaderData.Colspan = 2;
                            dateHeaderData.Border = Rectangle.BOX;
                            dateHeaderData.BorderColor = BaseColor.BLACK;
                            dateHeaderData.BorderWidth = 1;
                            dateHeaderData.DisableBorderSide(Rectangle.TOP_BORDER);
                            dateHeaderData.DisableBorderSide(Rectangle.LEFT_BORDER);
                            dateHeaderData.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            // dateHeaderData.Border = Rectangle.NO_BORDER;
                            dateHeaderData.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            acctable.AddCell(dateHeaderData);
                            document.Add(acctable);


                            PdfPTable headerTable = new PdfPTable(4);
                            headerTable.WidthPercentage = 100;
                            headerTable.HorizontalAlignment = Rectangle.ALIGN_CENTER;


                            PdfPCell centerAppHeader = new PdfPCell(new Phrase("Insurance Verification", fontBodyBold));

                            //PdfPCell centerAppHeader = new PdfPCell(new Phrase("Insurance Verification \n\n\nDOB \n\n\nDoctor \n\n\nPCP Phone \n\n\n\n\nUHC MCO 800-318-8821 \n\n\nUHC 887-778-6786 \n\n\n\n\n\n\n\nReferral Required \n\n\", fontBodyBold));
                            centerAppHeader.MinimumHeight = 20;
                            centerAppHeader.Colspan = 4;
                            centerAppHeader.Border = Rectangle.NO_BORDER;
                            centerAppHeader.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            centerAppHeader.PaddingTop = 5;
                            centerAppHeader.PaddingBottom = 20;
                            headerTable.AddCell(centerAppHeader);

                            document.Add(headerTable);

                            //Chunk chunk1 = new Chunk("0123456789");
                            //chunk1.SetUnderline(2, -3);
                            //document.Add(new Chunk("\n"));


                            PdfPTable patientpdfPTable = new PdfPTable(4);
                            patientpdfPTable.WidthPercentage = 100;
                            patientpdfPTable.HorizontalAlignment = Rectangle.ALIGN_LEFT;


                            //first row start

                            PdfPCell DateTitle = new PdfPCell(new Phrase("Date", header));
                            DateTitle.MinimumHeight = 20;
                            DateTitle.Border = Rectangle.NO_BORDER;
                            DateTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            DateTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(DateTitle);

                            string? currentDate = DateTime.Now.ToString("MM-dd-yyyy");
                            PdfPCell Datevalue = new PdfPCell(new Phrase(currentDate, cellValue));
                            Datevalue.MinimumHeight = 20;
                            Datevalue.Border = Rectangle.BOX;
                            Datevalue.BorderColor = BaseColor.BLACK;
                            Datevalue.BorderWidth = 1;
                            Datevalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            Datevalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            Datevalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            //Datevalue.Border = Rectangle.NO_BORDER;

                            Datevalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            /*Datevalue.PaddingLeft = 60;*/
                            patientpdfPTable.AddCell(Datevalue);

                            PdfPCell TaxtIditle = new PdfPCell(new Phrase("Tax ID", header));
                            TaxtIditle.MinimumHeight = 20;
                            TaxtIditle.Border = Rectangle.NO_BORDER;
                            TaxtIditle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            TaxtIditle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(TaxtIditle);

                            string taxId = appointmentDetails.TaxId != null ? Convert.ToString(appointmentDetails.TaxId) : "";

                            PdfPCell TaxIdvalue = new PdfPCell(new Phrase(taxId, cellValue));
                            TaxIdvalue.MinimumHeight = 20;
                            TaxIdvalue.Border = Rectangle.BOX;
                            TaxIdvalue.BorderColor = BaseColor.BLACK;
                            TaxIdvalue.BorderWidth = 1;
                            TaxIdvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            TaxIdvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            TaxIdvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            TaxIdvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                            patientpdfPTable.AddCell(TaxIdvalue);
                            //First row end 


                            //second row start

                            PdfPCell PatientNameTitle = new PdfPCell(new Phrase("Patient Name", header));
                            PatientNameTitle.MinimumHeight = 20;
                            PatientNameTitle.Border = Rectangle.NO_BORDER;
                            PatientNameTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            PatientNameTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(PatientNameTitle);

                            string patientName = Convert.ToString(appointmentDetails.PatientFirstName);
                            PdfPCell PatientNamevalue = new PdfPCell(new Phrase(patientName, cellValue));
                            PatientNamevalue.MinimumHeight = 20;
                            PatientNamevalue.Border = Rectangle.BOX;
                            PatientNamevalue.BorderColor = BaseColor.BLACK;
                            PatientNamevalue.BorderWidth = 1;
                            PatientNamevalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            PatientNamevalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            PatientNamevalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            //PatientNamevalue.Border = Rectangle.NO_BORDER;
                            PatientNamevalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(PatientNamevalue);

                            PdfPCell DOBTitle = new PdfPCell(new Phrase("DOB", header));
                            DOBTitle.MinimumHeight = 20;
                            DOBTitle.Border = Rectangle.NO_BORDER;
                            DOBTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            DOBTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(DOBTitle);

                            string? dob = appointmentDetails.PatientDob != null ? appointmentDetails.PatientDob.ToString("MM-dd-yyyy") : null;
                            PdfPCell DOBvalue = new PdfPCell(new Phrase(dob, cellValue));
                            DOBvalue.MinimumHeight = 20;
                            DOBvalue.Border = Rectangle.BOX;
                            DOBvalue.BorderColor = BaseColor.BLACK;
                            DOBvalue.BorderWidth = 1;
                            DOBvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            DOBvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            DOBvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            DOBvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(DOBvalue);
                            //second row end 

                            //third row start
                            PdfPCell PhoneTitle = new PdfPCell(new Phrase("Phone", header));
                            PhoneTitle.MinimumHeight = 20;
                            PhoneTitle.Border = Rectangle.NO_BORDER;
                            PhoneTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            PhoneTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(PhoneTitle);

                            string? phone = appointmentDetails.PatientMobileNo != null ? appointmentDetails.PatientMobileNo : "";
                            PdfPCell PhoneValue = new PdfPCell(new Phrase(String.Format("{0:###-###-####}", Convert.ToInt64(phone)), cellValue));
                            PhoneValue.MinimumHeight = 20;
                            PhoneValue.Border = Rectangle.BOX;
                            PhoneValue.BorderColor = BaseColor.BLACK;
                            PhoneValue.BorderWidth = 1;
                            PhoneValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            PhoneValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            PhoneValue.DisableBorderSide(Rectangle.RIGHT_BORDER);

                            PhoneValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(PhoneValue);

                            PdfPCell DoctorTitle = new PdfPCell(new Phrase("Doctor", header));
                            DoctorTitle.MinimumHeight = 20;
                            DoctorTitle.Border = Rectangle.NO_BORDER;
                            DoctorTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            DoctorTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(DoctorTitle);

                            var doctorDetails = _dbContext.PhysicianMsts.FirstOrDefault(x => x.Id == appointmentDetails.AppDoctorId);
                            string doctor = doctorDetails != null ? doctorDetails.DoctorFirstName + " " +doctorDetails.DoctorLastName : "";
                            PdfPCell DoctorValue = new PdfPCell(new Phrase(doctor, cellValue));
                            DoctorValue.MinimumHeight = 20;
                            DoctorValue.Border = Rectangle.BOX;
                            DoctorValue.BorderColor = BaseColor.BLACK;
                            DoctorValue.BorderWidth = 1;
                            DoctorValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            DoctorValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            DoctorValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            DoctorValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(DoctorValue);
                            //third row end

                            //forth row start
                            PdfPCell PCPTitle = new PdfPCell(new Phrase("PCP", header));
                            PCPTitle.MinimumHeight = 20;
                            PCPTitle.Border = Rectangle.NO_BORDER;
                            PCPTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            PCPTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(PCPTitle);

                            string pcp = appointmentDetails.Pcp != null ? appointmentDetails.Pcp : "";
                            PdfPCell PCPValue = new PdfPCell(new Phrase(pcp, cellValue));
                            PCPValue.MinimumHeight = 20;
                            PCPValue.Border = Rectangle.BOX;
                            PCPValue.BorderColor = BaseColor.BLACK;
                            PCPValue.BorderWidth = 1;
                            PCPValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            PCPValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            PCPValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            PCPValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(PCPValue);

                            PdfPCell PCCPhoneTitle = new PdfPCell(new Phrase("PCP Phone", header));
                            PCCPhoneTitle.MinimumHeight = 20;
                            PCCPhoneTitle.Border = Rectangle.NO_BORDER;
                            PCCPhoneTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            PCCPhoneTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(PCCPhoneTitle);

                            string pcpPhone = appointmentDetails.PcpmobileNo != null ? appointmentDetails.PcpmobileNo : "";
                            PdfPCell PCCPhonevalue = new PdfPCell(new Phrase(String.Format("{0:###-###-####}", Convert.ToInt64(pcpPhone)), cellValue));
                            PCCPhonevalue.MinimumHeight = 20;
                            PCCPhonevalue.Border = Rectangle.BOX;
                            PCCPhonevalue.BorderColor = BaseColor.BLACK;
                            PCCPhonevalue.BorderWidth = 1;
                            PCCPhonevalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            PCCPhonevalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            PCCPhonevalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            PCCPhonevalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(PCCPhonevalue);
                            //forth row end

                            //fifth row start
                            string ReferringMD = appointmentDetails.ReferingMd != null ? appointmentDetails.ReferingMd : "";
                            PdfPCell ReferringMDTitle = new PdfPCell(new Phrase("Referring M.D.", header));
                            ReferringMDTitle.MinimumHeight = 20;
                            ReferringMDTitle.Border = Rectangle.NO_BORDER;
                            ReferringMDTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            ReferringMDTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(ReferringMDTitle);

                            string referingMD = appointmentDetails.ReferingMd != null ? appointmentDetails.ReferingMd : "";
                            PdfPCell ReferringMDvalue = new PdfPCell(new Phrase(referingMD, cellValue));
                            ReferringMDvalue.MinimumHeight = 20;
                            ReferringMDvalue.Border = Rectangle.BOX;
                            ReferringMDvalue.BorderColor = BaseColor.BLACK;
                            ReferringMDvalue.BorderWidth = 1;
                            ReferringMDvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            ReferringMDvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            ReferringMDvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            ReferringMDvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(ReferringMDvalue);

                            PdfPCell blanktitle = new PdfPCell(new Phrase("", header));
                            blanktitle.MinimumHeight = 20;
                            blanktitle.Border = Rectangle.NO_BORDER;
                            blanktitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            blanktitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(blanktitle);

                            string referingMDphone = appointmentDetails.ReferingMobileNo != null ? appointmentDetails.ReferingMobileNo : "";
                            PdfPCell blankvalue = new PdfPCell(new Phrase(String.Format("{0:###-###-####}", Convert.ToInt64(referingMDphone)), cellValue));
                            blankvalue.MinimumHeight = 20;
                            blankvalue.Border = Rectangle.BOX;
                            blankvalue.BorderColor = BaseColor.BLACK;
                            blankvalue.BorderWidth = 1;
                            blankvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            blankvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            blankvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            blankvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            blankvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(blankvalue);
                            //fifth row end

                            //sixth row start
                            PdfPCell Insurancetitle = new PdfPCell(new Phrase("Insurance", header));
                            Insurancetitle.MinimumHeight = 20;
                            Insurancetitle.Border = Rectangle.NO_BORDER;
                            Insurancetitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            Insurancetitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(Insurancetitle);

                            string insurance = appointmentDetails.PrimaryInsuranceName != null ? appointmentDetails.PrimaryInsuranceName : "";
                            PdfPCell Insurancevalue = new PdfPCell(new Phrase(insurance, cellValue));
                            Insurancevalue.MinimumHeight = 20;
                            Insurancevalue.Border = Rectangle.BOX;
                            Insurancevalue.BorderColor = BaseColor.BLACK;
                            Insurancevalue.BorderWidth = 1;
                            Insurancevalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            Insurancevalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            Insurancevalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            Insurancevalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(Insurancevalue);

                            PdfPCell Insurancevalue2 = new PdfPCell(new Phrase(" ", header));
                            Insurancevalue2.MinimumHeight = 20;
                            Insurancevalue2.Colspan = 2;
                            Insurancevalue2.Border = Rectangle.NO_BORDER;
                            Insurancevalue2.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            Insurancevalue2.PaddingLeft = 30;
                            patientpdfPTable.AddCell(Insurancevalue2);

                            /*PdfPCell blankvalue = new PdfPCell(new Phrase("blank value "));
                            //Datevalue.Colspan = 2;
                            blankvalue.Border = Rectangle.NO_BORDER;
                            blankvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            blankvalue.PaddingLeft = 60;
                            patientpdfPTable.AddCell(blankvalue);*/
                            //sixth row end

                            //seventh row start
                            PdfPCell IDtitle = new PdfPCell(new Phrase("ID #", header));
                            IDtitle.MinimumHeight = 20;
                            IDtitle.Border = Rectangle.NO_BORDER;
                            IDtitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            IDtitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(IDtitle);

                            string insuranceId = appointmentDetails.PrimaryInsuranceId != null ? appointmentDetails.PrimaryInsuranceId : "";
                            PdfPCell IDValue = new PdfPCell(new Phrase(insuranceId, cellValue));
                            IDValue.MinimumHeight = 20;
                            IDValue.Border = Rectangle.BOX;
                            IDValue.BorderColor = BaseColor.BLACK;
                            IDValue.BorderWidth = 1;
                            IDValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            IDValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            IDValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            IDValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(IDValue);

                            PdfPCell IDValue2 = new PdfPCell(new Phrase("", header));
                            IDValue2.MinimumHeight = 20;
                            IDValue2.Colspan = 2;
                            IDValue2.Border = Rectangle.NO_BORDER;
                            IDValue2.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            IDValue2.PaddingLeft = 30;
                            patientpdfPTable.AddCell(IDValue2);

                            /*PdfPCell blankvalue = new PdfPCell(new Phrase("blank value "));
                            //Datevalue.Colspan = 2;
                            blankvalue.Border = Rectangle.NO_BORDER;
                            blankvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            blankvalue.PaddingLeft = 60;
                            patientpdfPTable.AddCell(blankvalue);*/
                            //seventh row end

                            //eight row start
                            PdfPCell eightrowblank = new PdfPCell(new Phrase(" ", header));
                            eightrowblank.MinimumHeight = 20;
                            eightrowblank.Border = Rectangle.NO_BORDER;
                            eightrowblank.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            eightrowblank.PaddingLeft = 30;
                            patientpdfPTable.AddCell(eightrowblank);

                            PdfPCell eightrowblank2 = new PdfPCell(new Phrase(" ", header));
                            eightrowblank2.MinimumHeight = 20;
                            eightrowblank2.Colspan = 3;
                            IDtitle.Border = Rectangle.BOX;
                            IDtitle.BorderColor = BaseColor.BLACK;
                            eightrowblank2.BorderWidth = 1;
                            eightrowblank2.DisableBorderSide(Rectangle.TOP_BORDER);
                            eightrowblank2.DisableBorderSide(Rectangle.LEFT_BORDER);
                            eightrowblank2.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            eightrowblank2.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(eightrowblank2);
                            //eight row end

                            //ninth row start
                            PdfPCell ninthrowblank = new PdfPCell(new Phrase(" ", header));
                            ninthrowblank.MinimumHeight = 20;
                            ninthrowblank.Border = Rectangle.NO_BORDER;
                            ninthrowblank.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            ninthrowblank.PaddingLeft = 30;
                            patientpdfPTable.AddCell(ninthrowblank);

                            PdfPCell ninthrowblank2 = new PdfPCell(new Phrase(" ", header));
                            ninthrowblank2.MinimumHeight = 20;
                            ninthrowblank2.Colspan = 3;
                            ninthrowblank2.Border = Rectangle.BOX;
                            ninthrowblank2.BorderColor = BaseColor.BLACK;
                            ninthrowblank2.BorderWidth = 1;
                            ninthrowblank2.DisableBorderSide(Rectangle.TOP_BORDER);
                            ninthrowblank2.DisableBorderSide(Rectangle.LEFT_BORDER);
                            ninthrowblank2.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            ninthrowblank2.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(ninthrowblank2);
                            //ninth row end


                            //tenth row start
                            PdfPCell Referraltitle = new PdfPCell(new Phrase("Referral", header));
                            Referraltitle.MinimumHeight = 20;
                            Referraltitle.Border = Rectangle.NO_BORDER;
                            Referraltitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            Referraltitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(Referraltitle);

                            string referral = "";
                            PdfPCell Referralvalue = new PdfPCell(new Phrase(referral, cellValue));
                            Referralvalue.MinimumHeight = 20;
                            Referralvalue.Border = Rectangle.BOX;
                            Referralvalue.BorderColor = BaseColor.BLACK;
                            Referralvalue.BorderWidth = 1;
                            Referralvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            Referralvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            Referralvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            Referralvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(Referralvalue);

                            PdfPCell Referralrequiredtitle = new PdfPCell(new Phrase("Referral required?", header));
                            Referralrequiredtitle.MinimumHeight = 20;
                            Referralrequiredtitle.Border = Rectangle.NO_BORDER;
                            Referralrequiredtitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            Referralrequiredtitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(Referralrequiredtitle);

                            string referralRequired = "";
                            PdfPCell Referralrequiredvalue = new PdfPCell(new Phrase(referralRequired, cellValue));
                            Referralrequiredvalue.MinimumHeight = 20;
                            Referralrequiredvalue.Border = Rectangle.BOX;
                            Referralrequiredvalue.BorderColor = BaseColor.BLACK;
                            Referralrequiredvalue.BorderWidth = 1;
                            Referralrequiredvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            Referralrequiredvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            Referralrequiredvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            Referralrequiredvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(Referralrequiredvalue);
                            //tenth row end

                            //eleventh row start
                            PdfPCell StartEndDateTitle = new PdfPCell(new Phrase("Start / End Date", header));
                            StartEndDateTitle.MinimumHeight = 20;
                            StartEndDateTitle.Border = Rectangle.NO_BORDER;
                            StartEndDateTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            StartEndDateTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(StartEndDateTitle);

                            string startEndDate = "";
                            PdfPCell StartEndDateValue = new PdfPCell(new Phrase(startEndDate, cellValue));
                            StartEndDateValue.MinimumHeight = 20;
                            StartEndDateValue.Border = Rectangle.BOX;
                            StartEndDateValue.BorderColor = BaseColor.BLACK;
                            StartEndDateValue.BorderWidth = 1;
                            StartEndDateValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            StartEndDateValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            StartEndDateValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            StartEndDateValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(StartEndDateValue);

                            PdfPCell PaperorElectronicTitle = new PdfPCell(new Phrase("Paper or Electronic", header));
                            PaperorElectronicTitle.MinimumHeight = 20;
                            PaperorElectronicTitle.Border = Rectangle.NO_BORDER;
                            PaperorElectronicTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            PaperorElectronicTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(PaperorElectronicTitle);

                            string paperorElectronic = "";
                            PdfPCell PaperorElectronicValue = new PdfPCell(new Phrase(paperorElectronic, cellValue));
                            PaperorElectronicValue.MinimumHeight = 20;
                            PaperorElectronicValue.Border = Rectangle.BOX;
                            PaperorElectronicValue.BorderColor = BaseColor.BLACK;
                            PaperorElectronicValue.BorderWidth = 1;
                            PaperorElectronicValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            PaperorElectronicValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            PaperorElectronicValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            PaperorElectronicValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(PaperorElectronicValue);
                            //eleventh row end

                            //twelveth row start
                            PdfPCell ofvisitsTitle = new PdfPCell(new Phrase("# of visits", header));
                            ofvisitsTitle.MinimumHeight = 20;
                            ofvisitsTitle.Border = Rectangle.NO_BORDER;
                            ofvisitsTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            ofvisitsTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(ofvisitsTitle);

                            string ofVisits = "";
                            PdfPCell ofvisitsvalue = new PdfPCell(new Phrase(ofVisits, cellValue));
                            ofvisitsvalue.MinimumHeight = 20;
                            ofvisitsvalue.Border = Rectangle.BOX;
                            ofvisitsvalue.BorderColor = BaseColor.BLACK;
                            ofvisitsvalue.BorderWidth = 1;
                            ofvisitsvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            ofvisitsvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            ofvisitsvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            ofvisitsvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(ofvisitsvalue);

                            PdfPCell specCopayTitle = new PdfPCell(new Phrase("Spec. Copay", header));
                            specCopayTitle.MinimumHeight = 20;
                            specCopayTitle.Border = Rectangle.NO_BORDER;
                            specCopayTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            specCopayTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(specCopayTitle);

                            string specCopay = "";
                            PdfPCell specCopayValue = new PdfPCell(new Phrase(specCopay, cellValue));
                            specCopayValue.MinimumHeight = 20;
                            specCopayValue.Border = Rectangle.BOX;
                            specCopayValue.BorderColor = BaseColor.BLACK;
                            specCopayValue.BorderWidth = 1;
                            specCopayValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            specCopayValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            specCopayValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            specCopayValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(specCopayValue);
                            //twelveth row end


                            //thirteen row start
                            PdfPCell ContactTitle = new PdfPCell(new Phrase("Contact", header));
                            ContactTitle.MinimumHeight = 20;
                            ContactTitle.Border = Rectangle.NO_BORDER;
                            ContactTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            ContactTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(ContactTitle);

                            string contact = "";
                            PdfPCell ContactValue = new PdfPCell(new Phrase(contact, cellValue));
                            ContactValue.MinimumHeight = 20;
                            ContactValue.Border = Rectangle.BOX;
                            ContactValue.BorderColor = BaseColor.BLACK;
                            ContactValue.BorderWidth = 1;
                            ContactValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            ContactValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            ContactValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            ContactValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(ContactValue);

                            PdfPCell DeductibleTitle = new PdfPCell(new Phrase("Deductible", header));
                            DeductibleTitle.MinimumHeight = 20;
                            DeductibleTitle.Border = Rectangle.NO_BORDER;
                            DeductibleTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            DeductibleTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(DeductibleTitle);

                            string deductible = "";
                            PdfPCell DeductibleValue = new PdfPCell(new Phrase(deductible, cellValue));
                            DeductibleValue.MinimumHeight = 20;
                            DeductibleValue.Border = Rectangle.BOX;
                            DeductibleValue.BorderColor = BaseColor.BLACK;
                            DeductibleValue.BorderWidth = 1;
                            DeductibleValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            DeductibleValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            DeductibleValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            DeductibleValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(DeductibleValue);
                            //thirteen row end

                            //fourteen row start
                            PdfPCell CallRefTitle = new PdfPCell(new Phrase("Call Ref #", header));
                            CallRefTitle.MinimumHeight = 20;
                            CallRefTitle.Border = Rectangle.NO_BORDER;
                            CallRefTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            CallRefTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(CallRefTitle);

                            string callRef = "";
                            PdfPCell CallRefValue = new PdfPCell(new Phrase(callRef, cellValue));
                            CallRefValue.MinimumHeight = 20;
                            CallRefValue.Border = Rectangle.BOX;
                            CallRefValue.BorderColor = BaseColor.BLACK;
                            CallRefValue.BorderWidth = 1;
                            CallRefValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            CallRefValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            CallRefValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            CallRefValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(CallRefValue);

                            PdfPCell DedMetTitle = new PdfPCell(new Phrase("Ded. Met", header));
                            DeductibleTitle.MinimumHeight = 20;
                            DedMetTitle.Border = Rectangle.NO_BORDER;
                            DedMetTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            DedMetTitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(DedMetTitle);

                            string dedMet = "";
                            PdfPCell DedMetValue = new PdfPCell(new Phrase(dedMet, cellValue));
                            DeductibleValue.MinimumHeight = 20;
                            DedMetValue.Border = Rectangle.BOX;
                            DedMetValue.BorderColor = BaseColor.BLACK;
                            DedMetValue.BorderWidth = 1;
                            DedMetValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            DedMetValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            DedMetValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            DedMetValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(DedMetValue);
                            //fourteen row end

                            //fifteen row start
                            PdfPCell Notestitle = new PdfPCell(new Phrase("NOTES", header));
                            Notestitle.MinimumHeight = 20;
                            Notestitle.Border = Rectangle.NO_BORDER;
                            Notestitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            Notestitle.PaddingLeft = 30;
                            patientpdfPTable.AddCell(Notestitle);


                            string notes = appointmentDetails.Notes != null ? appointmentDetails.Notes : " ";
                            PdfPCell Notesvalue = new PdfPCell(new Phrase(notes, header));
                            Notesvalue.MinimumHeight = 20;
                            Notesvalue.Border = Rectangle.BOX;
                            Notesvalue.BorderColor = BaseColor.BLACK;
                            Notesvalue.BorderWidth = 1;
                            Notesvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            Notesvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            Notesvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            Notesvalue.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                            Notesvalue.Colspan = 3;
                            Notesvalue.Border = Rectangle.NO_BORDER;
                            Notesvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            patientpdfPTable.AddCell(Notesvalue);


                            //string pcp = appointmentDetails.Pcp != null ? appointmentDetails.Pcp : "";
                            //PdfPCell PCPValue = new PdfPCell(new Phrase(pcp, cellValue));
                            //PCPValue.MinimumHeight = 20;
                            //PCPValue.Border = Rectangle.BOX;
                            //PCPValue.BorderColor = BaseColor.BLACK;
                            //PCPValue.BorderWidth = 1;
                            //PCPValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            //PCPValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            //PCPValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            //PCPValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            //patientpdfPTable.AddCell(PCPValue);




                           // string notes = appointmentDetails.Notes != null ? appointmentDetails.Notes : " ";
                            PdfPCell Notesvalue2 = new PdfPCell(new Phrase(" ", cellValue));
                            Notesvalue2.MinimumHeight = 20;
                            Notesvalue2.Colspan = 4;
                            Notesvalue2.Border = Rectangle.NO_BORDER;
                            Notesvalue2.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            Notesvalue2.PaddingLeft = 30;
                            patientpdfPTable.AddCell(Notesvalue2);
                            document.Add(patientpdfPTable);



                            PdfPTable summarytbl = new PdfPTable(8);
                            summarytbl.WidthPercentage = 100;
                            summarytbl.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                            //Add blankspace
                            PdfPCell blankSpace2 = new PdfPCell(new Phrase("\n\n\n", fontBodyBold));
                            blankSpace2.MinimumHeight = 20;
                            blankSpace2.Colspan = 8;
                            blankSpace2.Border = Rectangle.NO_BORDER;
                            blankSpace2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                            summarytbl.AddCell(blankSpace2);


                            //firteen row end

                            //sixteen row start
                            PdfPCell IsThisTitle = new PdfPCell(new Phrase("Is this appt related to a motor vehicle accident or a work injury?", header));
                            IsThisTitle.MinimumHeight = 20;
                            IsThisTitle.Colspan = 5;
                            IsThisTitle.Border = Rectangle.NO_BORDER;
                            IsThisTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            IsThisTitle.PaddingLeft = 30;
                            summarytbl.AddCell(IsThisTitle);

                            string? relatedtomotorvehicleaccident = appointmentDetails.IsAppoitmentVehicleOrworkInjury.Value == true ? "Yes" : "No";
                            PdfPCell IsThisValue = new PdfPCell(new Phrase(relatedtomotorvehicleaccident, cellValue));
                            IsThisValue.MinimumHeight = 20;
                            IsThisValue.Colspan = 3;
                            IsThisValue.Border = Rectangle.BOX;
                            IsThisValue.BorderColor = BaseColor.BLACK;
                            IsThisValue.BorderWidth = 1;
                            IsThisValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            IsThisValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            IsThisValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            IsThisValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            summarytbl.AddCell(IsThisValue);

                            //sixteen row end

                            //seventeen row start
                            PdfPCell EmailAddresstitle = new PdfPCell(new Phrase("Email Address", header));
                            EmailAddresstitle.MinimumHeight = 20;
                            EmailAddresstitle.Colspan = 2;
                            EmailAddresstitle.Border = Rectangle.NO_BORDER;
                            EmailAddresstitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            EmailAddresstitle.PaddingLeft = 30;
                            summarytbl.AddCell(EmailAddresstitle);

                            string? Email = appointmentDetails.PatientEmail != null ? appointmentDetails.PatientEmail : "";
                            PdfPCell EmailAddressvalue = new PdfPCell(new Phrase(Email, cellValue));
                            EmailAddressvalue.MinimumHeight = 20;
                            EmailAddressvalue.Colspan = 6;
                            EmailAddressvalue.Border = Rectangle.BOX;
                            EmailAddressvalue.BorderColor = BaseColor.BLACK;
                            EmailAddressvalue.BorderWidth = 1;
                            EmailAddressvalue.DisableBorderSide(Rectangle.TOP_BORDER);
                            EmailAddressvalue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            EmailAddressvalue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            EmailAddressvalue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            summarytbl.AddCell(EmailAddressvalue);

                            //seventeen row end


                            //eighteen row start
                            PdfPCell CovidTitle = new PdfPCell(new Phrase("Have you tested positive for Covid", header));
                            CovidTitle.MinimumHeight = 20;
                            CovidTitle.Colspan = 3;
                            CovidTitle.Border = Rectangle.NO_BORDER;
                            CovidTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            CovidTitle.PaddingLeft = 30;
                            summarytbl.AddCell(CovidTitle);

                            string? IscovidPositive = appointmentDetails.IsCovidPossitive.Value == true ? "Yes" : "No";
                            PdfPCell CovidValue = new PdfPCell(new Phrase(IscovidPositive, cellValue));
                            CovidValue.MinimumHeight = 20;
                            CovidValue.Border = Rectangle.BOX;
                            CovidValue.BorderColor = BaseColor.BLACK;
                            CovidValue.BorderWidth = 1;
                            CovidValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            CovidValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            CovidValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            CovidValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            summarytbl.AddCell(CovidValue);


                            PdfPCell vaccinatedTitle = new PdfPCell(new Phrase("Have you been vaccinated for Covid", header));
                            vaccinatedTitle.MinimumHeight = 20;
                            vaccinatedTitle.Colspan = 3;
                            vaccinatedTitle.Border = Rectangle.NO_BORDER;
                            vaccinatedTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            vaccinatedTitle.PaddingLeft = 30;
                            summarytbl.AddCell(vaccinatedTitle);

                            string? IsVaccinated = appointmentDetails.IsVaccinated.Value == true ? "Yes" : "No";
                            PdfPCell vaccinatedValue = new PdfPCell(new Phrase(IsVaccinated, cellValue));
                            vaccinatedValue.MinimumHeight = 20;
                            vaccinatedValue.Border = Rectangle.BOX;
                            vaccinatedValue.BorderColor = BaseColor.BLACK;
                            vaccinatedValue.BorderWidth = 1;
                            vaccinatedValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            vaccinatedValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            vaccinatedValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            vaccinatedValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            summarytbl.AddCell(vaccinatedValue);

                            //eighteen row end

                            //nineteen row start
                            PdfPCell IdExpiredTitle = new PdfPCell(new Phrase("Is your ID current/not expired?", header));
                            IdExpiredTitle.MinimumHeight = 20;
                            IdExpiredTitle.Colspan = 3;
                            IdExpiredTitle.Border = Rectangle.NO_BORDER;
                            IdExpiredTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            IdExpiredTitle.PaddingLeft = 30;
                            summarytbl.AddCell(IdExpiredTitle);

                            string? IsCurrentIdExpired = appointmentDetails.IsIdCurrentOrExpired != null ? appointmentDetails.IsIdCurrentOrExpired  : " ";
                            PdfPCell IdExpiredValue = new PdfPCell(new Phrase(IsCurrentIdExpired, cellValue));
                            IdExpiredValue.MinimumHeight = 20;
                            IdExpiredValue.Border = Rectangle.BOX;
                            IdExpiredValue.BorderColor = BaseColor.BLACK;
                            IdExpiredValue.BorderWidth = 1;
                            IdExpiredValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            IdExpiredValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            IdExpiredValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            IdExpiredValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            summarytbl.AddCell(IdExpiredValue);

                            PdfPCell TypeofIDTitle = new PdfPCell(new Phrase("Type of ID(Expiration Date)", header));
                            TypeofIDTitle.MinimumHeight = 20;
                            TypeofIDTitle.Colspan = 3;
                            TypeofIDTitle.Border = Rectangle.NO_BORDER;
                            TypeofIDTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            TypeofIDTitle.PaddingLeft = 30;
                            summarytbl.AddCell(TypeofIDTitle);

                            string? typeOfId = appointmentDetails.IdExpirationDate != null ? appointmentDetails.IdExpirationDate.ToString("MM-dd-yyyy") : null;
                            PdfPCell TypeofIDValue = new PdfPCell(new Phrase(typeOfId, cellValue));
                            TypeofIDValue.MinimumHeight = 20;
                            TypeofIDValue.Border = Rectangle.BOX;
                            TypeofIDValue.BorderColor = BaseColor.BLACK;
                            TypeofIDValue.BorderWidth = 1;
                            TypeofIDValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            TypeofIDValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            TypeofIDValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            TypeofIDValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            summarytbl.AddCell(TypeofIDValue);

                            //nineteen row end

                            //twenty row start
                            PdfPCell IDMatchTitle = new PdfPCell(new Phrase("Does your first and last name AND date of birth on your ID match EXACTLY with your \r\ninsurance ", header));
                            IdExpiredTitle.MinimumHeight = 30;
                            IDMatchTitle.Colspan = 7;
                            IDMatchTitle.Border = Rectangle.NO_BORDER;
                            IDMatchTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            IDMatchTitle.PaddingLeft = 30;
                            summarytbl.AddCell(IDMatchTitle);

                            string IsIDMatch = appointmentDetails.IsMatchInsurance.Value == true ? "Yes" : "No";
                            PdfPCell IDMatchValue = new PdfPCell(new Phrase(IsIDMatch, cellValue));
                            IDMatchValue.MinimumHeight = 30;
                            IDMatchValue.Border = Rectangle.BOX;
                            IDMatchValue.BorderColor = BaseColor.BLACK;
                            IDMatchValue.BorderWidth = 1;
                            IDMatchValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            IDMatchValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            IDMatchValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            IDMatchValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            summarytbl.AddCell(IDMatchValue);
                            //twenty row end

                            //twentyone row start
                            PdfPCell ReasonTitle = new PdfPCell(new Phrase("Reason for visit", header));
                            ReasonTitle.MinimumHeight = 20;
                            ReasonTitle.Colspan = 2;
                            ReasonTitle.Border = Rectangle.NO_BORDER;
                            ReasonTitle.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                            ReasonTitle.PaddingLeft = 30;
                            summarytbl.AddCell(ReasonTitle);

                            string? reason = appointmentDetails.Reason != null ? appointmentDetails.Reason : "";
                            PdfPCell ReasonValue = new PdfPCell(new Phrase(reason, cellValue));
                            ReasonValue.MinimumHeight = 20;
                            ReasonValue.Colspan = 7;
                            ReasonValue.Border = Rectangle.BOX;
                            ReasonValue.BorderColor = BaseColor.BLACK;
                            ReasonValue.BorderWidth = 1;
                            ReasonValue.DisableBorderSide(Rectangle.TOP_BORDER);
                            ReasonValue.DisableBorderSide(Rectangle.LEFT_BORDER);
                            ReasonValue.DisableBorderSide(Rectangle.RIGHT_BORDER);
                            ReasonValue.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                          
                            summarytbl.AddCell(ReasonValue);
                            //twentyone row end


                            document.Add(summarytbl);



                            pageSize.BackgroundColor = new BaseColor(234, 244, 251);
                            document.Close();
                            byte[] bytes = memoryStream.ToArray();
                            memoryStream.Close();
                            var datetime = DateTime.Now.ToString();
                            datetime = datetime.Replace(" ", "_");
                            datetime = datetime.Replace(":", "_");
                            datetime = datetime.Replace("-", "_");
                            datetime = datetime.Replace("/", "_");
                            var filename = "RCM_" + datetime + ".pdf";
                            var filePath1 = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files", "GenerateReports");

                            var exists = Directory.Exists(filePath1);
                            if (!exists)
                            {
                                Directory.CreateDirectory(filePath1);
                            }

                            filePath1 = System.IO.Path.Combine(filePath1.ToString(), filename);
                            File.WriteAllBytes(filePath1, bytes);


                            var FileBaseURL = Convert.ToString(_iConfiguration["FileBaseURL"]);
                            //var path = System.IO.Path.Combine(FileBaseURL, "Files", "GenerateReports", filename);
                            var path = "https://"+System.IO.Path.Combine(GetPhysicalRootPath(), "Files", "GenerateReports", filename);


                            commonResponse.Message = "Patient PDF Generated Successfully";
                            commonResponse.Status = true;
                            commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                            commonResponse.Data = path;

                        }
                    }
                    else
                    {
                        commonResponse.Message = "Please Enter Valid AppointmentId";
                        commonResponse.Status = false;
                        commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    commonResponse.Message = "Please Enter Valid UserId";
                    commonResponse.Status = false;
                    commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                }

            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
            }
            return commonResponse;

        }

        private string GetPhysicalRootPath()
        {
            string path = "";
            string ServerDomain = _httpContextAccessor.HttpContext.Request.Host.Value;
            var FileBaseURL = Convert.ToString(_iConfiguration["FileBaseURL"]);
            if (!string.IsNullOrWhiteSpace(ServerDomain))
            {
                path = ServerDomain;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(FileBaseURL))
                {
                    path = FileBaseURL;
                }
                else
                {
                    path = _hostingEnvironment.ContentRootPath;
                }
            }
            return path;


        }

        //public static Byte[] GeneratePdfFromFragment(string htmlFragment)
        //{
        //    var html = string.Format(@"
        //    <html xmlns='http://www.w3.org/1999/xhtml' xml:lang='en'>
        //    <head>
        //    <style type='text/css'>
        //    table,td {{border: 1px solid black;}}

        //    div {{ white-space: nowrap; padding: 2px;}}
        //    table{{ border-collapse: collapse; width: 100%; empty-cells: show;}}
        //    body table {{font-size: 50%;}}
        //    th {{width:500px; height: 28px;}}
        //    td {{width:300px; height: 28px;}}
        //    </style>
        //    </head><body>{0}</body></html>", htmlFragment);

        //    return generate(html);
        //}

        //public static Byte[] GeneratePdfFromPage(string htmlPage)
        //{
        //    return generate(htmlPage);
        //}

        //private static Byte[] generate(string html)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        var pdfDocument = new Document(PageSize.LETTER);
        //        var pdfWriter = PdfWriter.GetInstance(pdfDocument, memoryStream);
        //        pdfDocument.Open();
        //        using (var fw = new StringReader(html))
        //        {
        //             XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfDocument, fw);
        //            pdfDocument.Close();
        //            fw.Close();
        //        }
        //        return memoryStream.ToArray();
        //    }
        //}

    }

}

