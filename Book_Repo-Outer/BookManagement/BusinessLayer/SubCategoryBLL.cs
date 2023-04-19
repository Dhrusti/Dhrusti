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
    public class SubCategoryBLL
    {
        private readonly BookMgtDBContext _db;
        private readonly IMapper _mapper;
        public SubCategoryBLL(BookMgtDBContext db, IMapper mapper)
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

        public ResponseDTO GetSubCategory()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                List<TblSubCategoryMst> subCategoryList = new List<TblSubCategoryMst>();
                subCategoryList = _db.TblSubCategoryMsts.Where(x => x.IsDeleted == false).ToList();
                List<SubCategoryViewDTO> dtoList = (from s in _db.TblSubCategoryMsts
                                                    join
                                                    c in _db.TblCategoryMsts on s.CategoryId equals c.CategoryId
                                                    where (s.IsDeleted == false)

                                                    select new { s, c }).ToList().Select(x => new SubCategoryViewDTO
                                                    {
                                                        SubCategoryId = x.s.SubCategoryId,
                                                        CategoryId = x.c.CategoryId,
                                                        CategoryName = x.c.CategoryName,
                                                        SubCategoryName = x.s.SubCategoryName,
                                                    }).ToList();

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

        public ResponseDTO CreateSubCategory(SubCategoryDTO subcategory)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                TblSubCategoryMst subCategoryMst = new TblSubCategoryMst();
                var categorynameisexist = _db.TblSubCategoryMsts.Where(x => x.SubCategoryName == subcategory.SubCategoryName && x.CategoryId == subcategory.CategoryId && x.IsDeleted == false).ToList();
                if (categorynameisexist.Count <= 0)
                {
                    subCategoryMst.SubCategoryId = subcategory.SubCategoryId;
                    subCategoryMst.CategoryId = subcategory.CategoryId;
                    subCategoryMst.SubCategoryName = subcategory.SubCategoryName;
                    subCategoryMst.UpdateBy = 1;
                    subCategoryMst.CreatedBy = 1;
                    subCategoryMst.CreatedOn = DateTime.Now;
                    subCategoryMst.UpdateOn = DateTime.Now;

                    _db.TblSubCategoryMsts.Add(subCategoryMst);
                    _db.SaveChanges();

                    response.Data = _db.TblSubCategoryMsts;
                    response.Status = true;
                    response.Message = "Successfully Created...!!!";
                }
                else
                {
                    response.Status = false;
                    response.Message = "SubCategory Already Exists...!!!";
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

        public ResponseDTO UpdateSubCategory(int id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (id == null || id == 0)
                {
                    response.Message = "NotFound";
                }
                var obj = _db.TblSubCategoryMsts.Find(id);

                if (obj == null)
                {
                    response.Message = "NotFound";
                }
                SubCategoryDTO subcategory = new SubCategoryDTO();
                subcategory.SubCategoryId = obj.SubCategoryId;
                subcategory.CategoryId = obj.CategoryId;
                subcategory.SubCategoryName = obj.SubCategoryName;

                response.Data = subcategory;
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



        public ResponseDTO UpdateSubCategory(SubCategoryDTO subcategory)

        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var result = _db.TblSubCategoryMsts.Where(x => x.SubCategoryId == subcategory.SubCategoryId).FirstOrDefault();
                var res2 = _db.TblSubCategoryMsts.Where(c => c.SubCategoryName == subcategory.SubCategoryName && c.CategoryId == subcategory.CategoryId && c.IsDeleted == false).ToList();
                if (result != null && res2.Count <= 0)
                {
                    result.SubCategoryId = subcategory.SubCategoryId;
                    result.CategoryId = subcategory.CategoryId;
                    result.SubCategoryName = subcategory.SubCategoryName;
                    result.UpdateBy = 1;
                    result.CreatedBy = 1;
                    result.UpdateOn = DateTime.Now;

                    var res = _db.TblSubCategoryMsts.Update(result);
                    _db.SaveChanges();

                    response.Data = res;
                    response.Status = true;
                    response.Message = "Successfully Updated...!!!";
                }
                else
                {
                    response.Message = "SubCategory Already exit....!!";
                }
            }
            catch (Exception ex)
            {

                response.Data = ex;
                //response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO DeleteSubCategory(int Id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (Id <= 0)
                {
                    response.Message = "Not Found";
                }
                var del = _db.TblSubCategoryMsts.Find(Id);
                if (del == null)
                {
                    response.Message = "Not Found";
                }
                List<TblBookMst> bookmst = new List<TblBookMst>();
                bookmst = _db.TblBookMsts.Where(x => x.SubCategoryId == del.SubCategoryId).ToList();
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
                del.IsDeleted = true;
                del.IsActive = false;
                _db.Entry(del).State = EntityState.Modified;
                _db.SaveChanges();
                response.Message = "Success";
                response.Status = true;
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

