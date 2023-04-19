using AutoMapper;
using BookManagement.Models;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.IService;
using System.Diagnostics;

namespace BookManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _environment;
        private readonly IBookDownload _bookdownload;
        private readonly IMapper _mapper;
        private readonly IBook _book;
        private readonly IAuthentication _authentication;

        public HomeController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment, IAuthentication authentication, IBookDownload bookdownload, IBook book, IMapper mapper)
        {
            this._environment = environment;
            this._book = book;
            this._authentication = authentication;
            this._bookdownload = bookdownload;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult HomePage()
        {
            HttpContext.Session.Clear();
            List<ReportModel> report = new List<ReportModel>();
            var response = this._bookdownload.AllBooks();
            report = this._mapper.Map<List<ReportModel>>(response.Data);
            return View(report);
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BooksIndex()
        {
            ////List<BookListViewModel> bk=new List<BookListViewModel>();
            //var response = this._book.GetBook();
            //var bk = this._mapper.Map<List<BookViewModel>>(response.Data);

            var response2 = this._book.GetCategory();
            List<CategoryModel> model = new List<CategoryModel>();
            model = this._mapper.Map<List<CategoryModel>>(response2.Data);
            var context = model.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            List<SelectListItem> list = new List<SelectListItem>().ToList();
            ViewBag.categoryList = context;

            //var SCResponse = this._book.GetSubCategory(catid);
            //List<SubCategoryModel> SCmodel = new List<SubCategoryModel>();
            //SCmodel = this._mapper.Map<List<SubCategoryModel>>(SCResponse.Data);
            //var conte = SCmodel.Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.SubCategoryId.ToString() }).ToList();
            //List<SelectListItem> SClist = new List<SelectListItem>().ToList();
            //ViewBag.subcategoryList = conte;

            return View();
                
            //    List<BookViewModel> books = new List<BookViewModel>();
            //    var bookresponse = this._bookdownload.GetBooksById(catid,subcatid);
            //    books = this._mapper.Map<List<BookViewModel>>(bookresponse.Data);
            //    //return View("/Views/Book/BooksDetails.cshtml",books);
            //return View(books);
        }
        
        public ActionResult BooksDetails(int catid, int subcatid)
        {
            List<BookViewModel> books = new List<BookViewModel>();
            var bookresponse = this._bookdownload.GetBooksById(catid, subcatid);
            books = this._mapper.Map<List<BookViewModel>>(bookresponse.Data);
            return View("~/Views/Book/BooksDetails.cshtml",books);
            //return View(books);
        }

        public IActionResult BooksIndex2(int? id)
        {
            var response2 = this._book.GetSubCategory(id);
            List<SubCategoryModel> model = new List<SubCategoryModel>();
            model = this._mapper.Map<List<SubCategoryModel>>(response2.Data);
            var context = model.Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.SubCategoryId.ToString() }).ToList();
            return Json(context);
        }


        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult ViewBook(int id)
        {
            var user = HttpContext.Session.GetObject<UserMstModel>("User");
            int userId;
            if(user == null)
            {
               userId = 0;
            }
            else
            {
                userId = user.UserId;
            }
            
            BookViewModel bookdata = new BookViewModel();
            var bookresponse = _bookdownload.GetBookById(id);
            bookdata = _mapper.Map<BookViewModel>(bookresponse.Data);
            BookDownloadModel model = new BookDownloadModel();
            model.BookId = id;
            model.BookName = bookdata.BookName;
            model.AuthorName = bookdata.AuthorName;
            model.price = bookdata.Price;
            model.pdfpath = bookdata.Pdfpath;
            model.coverimage = bookdata.CoverImagePath;
            model.Publisher = bookdata.Publisher;
            if (userId!=0)
            {
                var userresponse = _authentication.GetUsersById(userId);
                UserMstModel userdata=new UserMstModel();
                userdata = _mapper.Map<UserMstModel>(userresponse.Data);
                string fullName = userdata.FullName;
                string lastName;
                var names = fullName.Split(' ');
                if(names.Length<=1)
                {
                    lastName = null;
                }
                else
                {
                    lastName = names[1];
                }
                string firstName = names[0];
                

                model.FirstName = firstName;
                model.LastNane = lastName;
                model.EmailId = userdata.Email;
                model.ContactNumber= userdata.ContactNumber;
                model.Location = userdata.Address;
                return View(model);
                
                
            }
            else
            {
                return View(model);
            }
            
            
            
            //return View(bookdata);
        }
        [HttpGet]
        public IActionResult DownloadBook(int id)
        {
            if (id > 0)
            {
                BookViewModel bookmodel = new BookViewModel();
                var bookresponse = _bookdownload.GetBookById(id);
                bookmodel = _mapper.Map<BookViewModel>(bookresponse.Data);
                BookDownloadModel model = new BookDownloadModel();
                model.BookId = id;
                model.BookName = bookmodel.BookName;
                model.AuthorName = bookmodel.AuthorName;
                model.price = bookmodel.Price;
                model.pdfpath = bookmodel.Pdfpath;
                model.Publisher = bookmodel.Publisher;
                return View(model);
            }
            else
            {
                return RedirectToAction("BooksIndex");
            }

        }
        [HttpPost]
        public JsonResult DownloadBook(BookDownloadModel model)
        {
            model.UpdateBy = 1;
            model.CreatedBy = 1;
            var user = HttpContext.Session.GetObject<UserMstModel>("User");
            var response = _bookdownload.AddDownload(_mapper.Map<BookDownloadDTO>(model));
            if (response.Status == true)
            {
                if (user == null)
                {
                    return Json(new
                    {
                        returnmodel = model.pdfpath,
                        redirectUrl = Url.Action("BooksIndex", "Home"),
                        isRedirect = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        returnmodel = model.pdfpath,
                        redirectUrl = Url.Action("BookIndex", "Book"),
                        isRedirect = true
                    });
                }
            }
            else
            {
                return Json(new
                {
                    isRedirect = false
                });
            }
        }

        

    }
}