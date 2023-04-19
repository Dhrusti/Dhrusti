using AutoMapper;
using BookManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ServiceLayer.IService;

namespace BookManagement.Controllers
{
    public class ReportController : Controller
    {
        private readonly IBookDownload _bookdownload;
        private readonly IMapper _mapper;

        public ReportController(IBookDownload bookDownload,IMapper mapper)
        {
            this._bookdownload = bookDownload;
            this._mapper = mapper;
        }
        public IActionResult ReportIndex()
        {
            List<ReportModel> report = new List<ReportModel>();
            var response = this._bookdownload.Report();
            report = this._mapper.Map<List<ReportModel>>(response.Data);
            return new ViewAsPdf(report);
        }
        //public IActionResult BooksReport()
        //{
        //    List<ReportModel> report=new List<ReportModel>();
        //    var response = this._bookdownload.Report();
        //    report = this._mapper.Map<List<ReportModel>>(response.Data);
            
        //    //return report;
        //}
    }
}
