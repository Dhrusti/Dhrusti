using AutoMapper;
using BookManagement.Models;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace BookManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _icategory;
        private readonly IMapper _mapper;
        public CategoryController(ICategory icategory, IMapper _mapper)
        {
            this._icategory = icategory;
            this._mapper = _mapper;
        }
        public IActionResult CategoryIndex(int pg = 1)
        {

           

            var response = this._icategory.GetCategory();
            response.Data = this._mapper.Map<List<CategoryModel>>(response.Data);
            var rec = response.Data;


            //const int pageSize = 10;
            //if (pg < 1)
            //    pg = 1;

            //int recsCount = rec.Count;
            //var pagination = new Pagination(recsCount, pg, pageSize);


            //int recSkip = (pg - 1) * pageSize;

            ////var data = rec.Skip(recSkip).Take(pagination.PageSize).ToList();
            //this.ViewBag.Pagination = pagination;

            return View(response.Data);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _icategory.CreateCategory(_mapper.Map<CategoryDTO>(model));
                if(response.Status == false)
                {
                    ViewBag.Message = response.Message;
                    return View();
                }
                else
                {
                    return RedirectToAction("CategoryIndex");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var response = _icategory.UpdateCategory(id);
            response.Data = this._mapper.Map<CategoryModel>(response.Data);
            return View(response.Data);
        }

        [HttpPost]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _icategory.UpdateCategory(_mapper.Map<CategoryDTO>(model));
                if (response.Status == false)
                {
                    ViewBag.Message = response.Message;
                    return View();
                }
                else
                {
                    return RedirectToAction("CategoryIndex");
                }
            }
            return View(model);
        }
        public IActionResult DeleteCategory(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Id Not Found");
            }
            var response = _icategory.DeleteCategory(Id);
            return RedirectToAction("CategoryIndex");
        }

    }
}
