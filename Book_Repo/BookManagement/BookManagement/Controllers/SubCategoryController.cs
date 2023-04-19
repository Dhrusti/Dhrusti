using AutoMapper;
using BookManagement.Models;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.IService;

namespace BookManagement.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategory _isubcategory;
        private readonly ICategory _icategory;

        private readonly IMapper _mapper;
        public SubCategoryController(ISubCategory isubcategory, IMapper _mapper, ICategory icategory)
        {
            this._isubcategory = isubcategory;
            this._icategory = icategory;
            this._mapper = _mapper;
        }
        public IActionResult SubCategoryIndex()
        {
            var response = this._isubcategory.GetSubCategory();
            response.Data = this._mapper.Map<List<SubCategoryModel>>(response.Data);
            return View(response.Data);
        }

        public IActionResult CreateSubCategory()
        {
            var response = this._isubcategory.GetCategory();
            List<CategoryModel> model = new List<CategoryModel>();
            model = this._mapper.Map<List<CategoryModel>>(response.Data);

            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem Items = new SelectListItem();
            Items.Text = "Select";
            Items.Value = "0";
            listItems.Add(Items);
            var list = model.Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName }).ToList();
            listItems.AddRange(list);

            ViewBag.SubCategoryList = listItems;
            return View();
        }

        [HttpPost]
        public IActionResult CreateSubCategory(SubCategoryModel model)
        {

            var response2 = this._isubcategory.GetCategory();
            List<CategoryModel> model1 = new List<CategoryModel>();
            model1 = this._mapper.Map<List<CategoryModel>>(response2.Data);

            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem Items = new SelectListItem();
            Items.Text = "Select";
            Items.Value = "0";
            listItems.Add(Items);
            var list = model1.Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName }).ToList();
            listItems.AddRange(list);

            ViewBag.SubCategoryList = listItems;
            if (ModelState.IsValid)
            {
                var response = _isubcategory.CreateSubCategory(_mapper.Map<SubCategoryDTO>(model));
                if (response.Status == false)
                {
                    ViewBag.Message = response.Message;
                    return View();
                }
                else
                {
                    return RedirectToAction("SubCategoryIndex");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateSubCategory(int id)
        {
            var response = _isubcategory.UpdateSubCategory(id);
            response.Data = this._mapper.Map<SubCategoryViewModel>(response.Data);
            var response2 = this._isubcategory.GetCategory();
            List<CategoryModel> model = new List<CategoryModel>();
            model = this._mapper.Map<List<CategoryModel>>(response2.Data);

            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem Items = new SelectListItem();
            Items.Text = "Select";
            Items.Value = "0";
            listItems.Add(Items);
            var list = model.Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName }).ToList();
            listItems.AddRange(list);

            ViewBag.SubCategoryList = listItems;
            return View(response.Data);
        }

        [HttpPost]
        public IActionResult UpdateSubCategory(SubCategoryViewModel model)
        {
            var response2 = this._isubcategory.GetCategory();
            List<CategoryModel> model2 = new List<CategoryModel>();
            model2 = this._mapper.Map<List<CategoryModel>>(response2.Data);

            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem Items = new SelectListItem();
            Items.Text = "Select";
            Items.Value = "0";
            listItems.Add(Items);
            var list = model2.Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName }).ToList();
            listItems.AddRange(list);
            ViewBag.SubCategoryList = listItems;

            if (ModelState.IsValid)
            {
                var response = _isubcategory.UpdateSubCategory(_mapper.Map<SubCategoryDTO>(model));
                if (response.Status == false)
                {
                    ViewBag.Message = response.Message;
                    return View();
                }
                else
                {
                    return RedirectToAction("SubCategoryIndex");
                }
            }
            return View(model);
        }
        public IActionResult DeleteSubCategory(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Id Not Found");
            }
            var response = _isubcategory.DeleteSubCategory(Id);
            return RedirectToAction("SubCategoryIndex");
        }

    }
}
