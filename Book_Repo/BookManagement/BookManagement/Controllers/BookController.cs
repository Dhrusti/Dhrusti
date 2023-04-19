using AutoMapper;
using BookManagement.Models;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.IService;

namespace BookManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _ibook;
        private readonly IMapper _mapper;

        public BookController(IBook _ibook, IMapper _mapper)
        {
            this._ibook = _ibook;
            this._mapper = _mapper;
        }
        public IActionResult BookIndex()
        {
            var response = this._ibook.GetBook();
            response.Data = this._mapper.Map<List<BookViewModel>>(response.Data);
            return View(response.Data);
        }

        [HttpGet]
        public IActionResult CreateBook(int? id)
        {

            var response2 = this._ibook.GetCategory();
            List<CategoryModel> model = new List<CategoryModel>();
            model = this._mapper.Map<List<CategoryModel>>(response2.Data);
            var context = model.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            List<SelectListItem> list = new List<SelectListItem>().ToList();
            ViewBag.categoryList = context;

            var SCResponse = this._ibook.GetSubCategory(id);
            List<SubCategoryModel> SCmodel = new List<SubCategoryModel>();
            SCmodel = this._mapper.Map<List<SubCategoryModel>>(SCResponse.Data);
            var conte = SCmodel.Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.SubCategoryId.ToString() }).ToList();
            List<SelectListItem> SClist = new List<SelectListItem>().ToList();
            ViewBag.subcategoryList = conte;

            return View();
        }

        public IActionResult BooksIndex2(int? id)
        {
            var response2 = this._ibook.GetSubCategory(id);
            List<SubCategoryModel> model = new List<SubCategoryModel>();
            model = this._mapper.Map<List<SubCategoryModel>>(response2.Data);
            var context = model.Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.SubCategoryId.ToString() }).ToList();

            return Json(context);

        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public ActionResult CreateBook([FromForm] BookModel model)
        {
            var response3 = this._ibook.GetCategory();
            List<CategoryModel> model1 = new List<CategoryModel>();
            model1 = this._mapper.Map<List<CategoryModel>>(response3.Data);
            var context = model1.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            List<SelectListItem> list = new List<SelectListItem>().ToList();
            ViewBag.categoryList = context;

            var SCResponse = this._ibook.GetSubCategory(model.CategoryId);
            List<SubCategoryModel> SCmodel = new List<SubCategoryModel>();
            SCmodel = this._mapper.Map<List<SubCategoryModel>>(SCResponse.Data);
            var conte = SCmodel.Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.SubCategoryId.ToString() }).ToList();
            List<SelectListItem> SClist = new List<SelectListItem>().ToList();
            ViewBag.subcategoryList = conte;

            if (ModelState.IsValid)
            {

                var response2 = _ibook.CreateBook(_mapper.Map<BookDTO>(model));
                if (response2.Status == false)
                {
                    ViewBag.Message = response2.Message;
                    return View();
                }
                else
                {
                    return RedirectToAction("BookIndex");
                }
            }
            return View();
        }


        public IActionResult Deletedbook(int Id)
        {

            if (Id == 0)
            {
                return BadRequest("Id Not Found");
            }
            var response = _ibook.DeleteBook(Id);
            return RedirectToAction("BookIndex");
        }

        [HttpGet]
        public IActionResult Editbook(int id)
        {

            

            var response = _ibook.Editbook(id);
            var res = this._mapper.Map<EditBookModel>(response.Data);

            var cat = res.CategoryId;



            var response2 = this._ibook.GetCategory();
            List<CategoryModel> model = new List<CategoryModel>();
            model = this._mapper.Map<List<CategoryModel>>(response2.Data);
            var context = model.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            List<SelectListItem> list = new List<SelectListItem>().ToList();
            ViewBag.categoryList = context;

            var SCResponse = this._ibook.GetSubCategory(cat);
            List<SubCategoryModel> SCmodel = new List<SubCategoryModel>();
            SCmodel = this._mapper.Map<List<SubCategoryModel>>(SCResponse.Data);
            var conte = SCmodel.Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.SubCategoryId.ToString() }).ToList();
            List<SelectListItem> SClist = new List<SelectListItem>().ToList();
            ViewBag.subcategoryList = conte;

            return View(res);
        }

        [HttpPost]
        //[Consumes("multipart/form-data")]
        public IActionResult Editbook(EditBookModel model)
        {
            var response3 = this._ibook.GetCategory();
            List<CategoryModel> model1 = new List<CategoryModel>();
            model1 = this._mapper.Map<List<CategoryModel>>(response3.Data);
            var context = model1.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            List<SelectListItem> list = new List<SelectListItem>().ToList();
            ViewBag.categoryList = context;

            var SCResponse = this._ibook.GetSubCategory(model.CategoryId);
            List<SubCategoryModel> SCmodel = new List<SubCategoryModel>();
            SCmodel = this._mapper.Map<List<SubCategoryModel>>(SCResponse.Data);
            var conte = SCmodel.Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.SubCategoryId.ToString() }).ToList();
            List<SelectListItem> SClist = new List<SelectListItem>().ToList();
            ViewBag.subcategoryList = conte;
            if (ModelState.IsValid)
            {
                var response2 = _ibook.Editbooks(_mapper.Map<BookViewDTO>(model));
                return RedirectToAction("BookIndex");
            }

            return View();

        }


    }
}