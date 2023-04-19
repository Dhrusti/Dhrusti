
using AutoMapper;
using DataLayer.Entities;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CategoryBLL
    {
        private readonly BookMgtDBContext _db;
        private readonly IMapper _mapper;
        public CategoryBLL(BookMgtDBContext db, IMapper mapper)
        {
            _db = db;
            this._mapper = mapper;
        }
        public ResponseDTO GetCategory()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                List<TblCategoryMst> CategoryList = new List<TblCategoryMst>();
                CategoryList = _db.TblCategoryMsts.Where(x => x.IsDeleted == false).ToList();
                List<CategoryDTO> dtoList = CategoryList.Select(p => new CategoryDTO
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName,
                }).OrderBy(x => x.CategoryId).ToList();

                response.Data = dtoList;
                //response.Data = this._mapper.Map<List<UserDTO>>(response.Data);
                response.Message = "Success";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
            }
            return response;
        }

        public ResponseDTO CreateCategory(CategoryDTO category)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                TblCategoryMst CategoryMst = new TblCategoryMst();
                var categorynameisexist = _db.TblCategoryMsts.Where(x => x.CategoryName == category.CategoryName && x.IsDeleted == false).ToList();
                if (categorynameisexist.Count <= 0)
                {
                    CategoryMst.CategoryId = category.CategoryId;
                    CategoryMst.CategoryName = category.CategoryName;
                    CategoryMst.UpdateBy = 1;
                    CategoryMst.CreatedBy = 1;
                    CategoryMst.CreatedOn = DateTime.Now;
                    CategoryMst.UpdateOn = DateTime.Now;

                    _db.TblCategoryMsts.Add(CategoryMst);
                    _db.SaveChanges();
                    response.Data = _db.TblCategoryMsts;
                    response.Status = true;
                    response.Message = "Successfully Created...!!!";

                }
                else
                {
                    response.Status = false;
                    response.Message = "Category Already Exist...!!!";
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


        public ResponseDTO UpdateCategory(int id)

        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (id == null || id == 0)
                {
                    response.Message = "NotFound";
                }
                var obj = _db.TblCategoryMsts.Find(id);

                if (obj == null)
                {
                    response.Message = "NotFound";
                }
                CategoryDTO category = new CategoryDTO();
                category.CategoryId = obj.CategoryId;
                category.CategoryName = obj.CategoryName;

                response.Data = category;
                response.Status = true;

            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO UpdateCategory(CategoryDTO category)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var result = _db.TblCategoryMsts.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();
                var categorynameisexist = _db.TblCategoryMsts.Where(x => x.CategoryName == category.CategoryName && x.IsDeleted == false).ToList();

                if (result != null && categorynameisexist.Count <= 0)
                {
                    result.CategoryId = category.CategoryId;
                    result.CategoryName = category.CategoryName;
                    result.UpdateBy = 1;
                    result.CreatedBy = 1;
                    result.UpdateOn = DateTime.Now;

                    var res = _db.TblCategoryMsts.Update(result);
                    _db.SaveChanges();

                    response.Data = res;
                    response.Status = true;
                    response.Message = "Successfully Updated...!!!";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Category Already Exist...!!!";
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

        public ResponseDTO DeleteCategory(int id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (id <= 0)
                {
                    response.Message = "Not Found";
                }
                var del = _db.TblCategoryMsts.Find(id);
                if (del == null)
                {
                    response.Message = "Not Found";
                }
                List<TblBookMst> bookmst = new List<TblBookMst>();
                bookmst = _db.TblBookMsts.Where(x => x.CategoryId == del.CategoryId).ToList();
                if (bookmst.Count > 0)
                {
                    foreach (var item in bookmst)
                    {
                        item.IsDeleted = true;
                        item.IsActive = false;
                        _db.Entry(item).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                List<TblSubCategoryMst> subcatmst = new List<TblSubCategoryMst>();
                subcatmst = _db.TblSubCategoryMsts.Where(x => x.CategoryId == del.CategoryId).ToList();
                if (subcatmst.Count > 0)
                {
                    foreach (var item in subcatmst)
                    {
                        item.IsDeleted = true;
                        item.IsActive = false;
                        _db.Entry(item).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                del.IsDeleted = true;
                del.IsActive = false;
                _db.Entry(del).State = EntityState.Modified;
                _db.SaveChanges();
                response.Status = true;
                response.Message = "Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
