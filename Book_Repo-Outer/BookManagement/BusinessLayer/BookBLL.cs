
using AutoMapper;
using DataLayer.Entities;
using DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer
{
    public class BookBLL
    {
        private readonly BookMgtDBContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookBLL(BookMgtDBContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        public ResponseDTO GetBook()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                List<TblBookMst> BookList = new List<TblBookMst>();
                BookList = _db.TblBookMsts.Where(x => x.IsDeleted == false).ToList();
                List<BookViewDTO> dtoList = BookList.Select(p => new BookViewDTO
                {
                    BookId = p.BookId,
                    BookName = p.BookName,
                    AuthorName = p.AuthorName,
                    Publisher = p.Publisher,
                    CoverImagePath = p.CoverImagePath
                }).ToList();

                response.Data = dtoList;
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

        public ResponseDTO CreateBook([FromForm] BookDTO book)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                TblBookMst tbl = new TblBookMst();
                var Isexitsalready = _db.TblBookMsts.Where(c => c.CategoryId == book.CategoryId && c.SubCategoryId == book.SubCategoryId && c.BookName == book.BookName && c.IsDeleted == false).ToList();

                if (Isexitsalready.Count <= 0)
                {
                    if (book.CoverImagePath.Length >= 0)
                    {
                        var fileExt = Path.GetFileName(book.CoverImagePath.FileName).ToLower();
                        var fileExt1 = Path.GetExtension(book.CoverImagePath.FileName).ToLower();
                        var fileExt2 = Path.GetFileName(book.Pdfpath.FileName).ToLower();
                        var fileExt3 = Path.GetExtension(book.Pdfpath.FileName).ToLower();
                        Guid guid = Guid.NewGuid();
                        var fileName = guid + fileExt; //Create a new Name for the file due to security reasons.
                        var pdfileName = guid + fileExt2;

                        string path = Path.Combine(this._webHostEnvironment.WebRootPath, "Images");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string path2 = Path.Combine(this._webHostEnvironment.WebRootPath, "Files");
                        if (!Directory.Exists(path2))
                        {
                            Directory.CreateDirectory(path2);
                        }

                        var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", fileName);
                        var pathBuilt2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\", pdfileName);


                        if (fileExt1 != ".jpg" && fileExt1 != ".png")
                        {
                            response.Status = false;
                            response.Message = "Image does not support,Plz upload in jpg and png format..";

                        }
                        else if (fileExt3 != ".pdf")
                        {
                            response.Status = false;
                            response.Message = "File does not support,Plz upload in PDF format..";
                        }
                        else
                        {
                            var pathBuilt1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", fileName);
                            var pathBuilt3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\", pdfileName);

                            tbl.CoverImagePath = fileName;
                            tbl.Pdfpath = pdfileName;
                            tbl.BookId = book.BookId;
                            tbl.BookName = book.BookName;
                            tbl.CategoryId = book.CategoryId;
                            tbl.SubCategoryId = book.SubCategoryId;
                            tbl.AuthorName = book.AuthorName;
                            tbl.BookPages = book.BookPages;
                            tbl.Publisher = book.Publisher;
                            tbl.PublishDate = book.PublishDate;
                            tbl.Edition = book.Edition;
                            tbl.Description = book.Description;
                            tbl.Price = book.Price;
                            tbl.UpdateBy = 1;
                            tbl.CreatedBy = 1;
                            tbl.CreatedOn = DateTime.Now;
                            tbl.UpdateOn = DateTime.Now;
                            tbl.IsActive = true;
                            tbl.IsDeleted = false;

                            var result = _db.TblBookMsts.Add(tbl);
                            _db.SaveChanges();

                            if (result != null)
                            {
                                response.Data = tbl;
                                response.Message = "Document uploaded Succesfully";
                                response.Status = true;
                            }

                            using (var fileSrteam = new FileStream(pathBuilt1, FileMode.Create))
                            {
                                book.CoverImagePath.CopyTo(fileSrteam);
                            }
                            using (var fileSrteam2 = new FileStream(pathBuilt3, FileMode.Create))
                            {
                                book.Pdfpath.CopyTo(fileSrteam2);
                            }
                        }
                    }
                    else
                    {
                        //  response.Data = tbl;
                        response.Message = "File does nor exist...";
                        response.Status = false;
                    }

                }
                else
                {
                    response.Status = false;
                    response.Message = "Book Already Exists...!!!";
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


        public ResponseDTO GetSubCategory(int? id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                List<TblSubCategoryMst> subCategoryList = new List<TblSubCategoryMst>();
                subCategoryList = _db.TblSubCategoryMsts.Where(x => x.IsDeleted == false).ToList();
                if (id.HasValue)
                {
                    List<SubCategoryDTO> dtoList = (from
                                                   c in _db.TblSubCategoryMsts
                                                    where c.CategoryId == id && c.IsDeleted == false
                                                    select new { c }).ToList().Select(x => new SubCategoryDTO
                                                    {
                                                        SubCategoryId = x.c.SubCategoryId,
                                                        CategoryId = x.c.CategoryId,
                                                        SubCategoryName = x.c.SubCategoryName,
                                                    }).ToList();
                    response.Data = dtoList;
                    response.Message = "Success";
                    response.Status = true;
                }
                else
                {
                    List<SubCategoryDTO> dtoList = (from
                                                   c in _db.TblSubCategoryMsts
                                                    where c.IsDeleted == false
                                                    select new { c }).ToList().Select(x => new SubCategoryDTO
                                                    {
                                                        SubCategoryId = x.c.SubCategoryId,
                                                        CategoryId = x.c.CategoryId,
                                                        SubCategoryName = x.c.SubCategoryName,
                                                    }).ToList();
                    response.Data = dtoList;
                    response.Message = "Passs";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
            }
            return response;
        }

        public ResponseDTO DeleteBook(int Id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (Id > 0)
                {
                    TblBookMst mst = new TblBookMst();
                    mst = _db.TblBookMsts.FirstOrDefault(x => x.BookId == Id);
                    if (mst != null)
                    {
                        mst.IsDeleted = true;
                        mst.IsActive = false;
                        _db.Entry(mst).State = EntityState.Modified;
                        _db.SaveChanges();
                        response.Status = true;
                        response.Message = "Delete Deleted Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO Editbook(int id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (id == null || id == 0)
                {
                    response.Message = "NotFound";
                }
                var obj = _db.TblBookMsts.Find(id);

                if (obj == null)
                {
                    response.Message = "NotFound";
                }

                BookViewDTO book = new BookViewDTO();
                book.BookId = obj.BookId;
                book.BookName = obj.BookName;
                book.Edition = obj.Edition;
                book.Description = obj.Description;
                book.AuthorName = obj.AuthorName;
                book.BookPages = obj.BookPages;
                book.CategoryId = obj.CategoryId;
                book.SubCategoryId = obj.SubCategoryId;
                book.Price = obj.Price;
                book.Publisher = obj.Publisher;
                book.PublishDate = obj.PublishDate;
                book.CoverImagePath = obj.CoverImagePath;
                book.Pdfpath = obj.Pdfpath;

                response.Data = book;
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

        public ResponseDTO Editbook(BookViewDTO book)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var result = _db.TblBookMsts.Where(x => x.BookId == book.BookId).FirstOrDefault();
                if (result != null)
                {

                    result.BookId = book.BookId;
                    result.BookName = book.BookName;
                    result.CategoryId = book.CategoryId;
                    result.SubCategoryId = book.SubCategoryId;
                    result.AuthorName = book.AuthorName;
                    result.BookPages = book.BookPages;
                    result.Price = book.Price;  
                    result.Publisher = book.Publisher;
                    result.PublishDate = book.PublishDate;
                    result.Edition = book.Edition;
                    result.Description = book.Description;
                    result.IsActive = true;
                    result.IsDeleted = false;
                    result.UpdateBy = 1;
                    result.CreatedBy = 1;
                    result.UpdateOn = DateTime.Now;

                    var res = _db.TblBookMsts.Update(result);
                    _db.SaveChanges();

                    response.Data = res;
                    response.Status = true;
                    response.Message = "Successfully Updated...!!!";
                }
                else
                {
                    response.Status = false;
                    response.Message = "UnSuccessfully Updated...!!!";
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
    }
}
