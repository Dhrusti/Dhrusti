using DataLayer.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BookDownloadBLL
    {
        private readonly BookMgtDBContext _context;
        public BookDownloadBLL(BookMgtDBContext context)
        {
            this._context = context;
        }
        public ResponseDTO DownloaderDetails(BookDownloadDTO book)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                
                TblBookDownloadMst tbl=new TblBookDownloadMst();
                tbl.BookId = book.BookId;
                tbl.FirstName = book.FirstName;
                tbl.LastNane=book.LastNane;
                tbl.EmailId= book.EmailId;
                
                tbl.ContactNumber = book.ContactNumber;
                tbl.Location= book.Location;
                tbl.CreatedBy= book.CreatedBy;
                tbl.CreatedOn= DateTime.Now;
                tbl.UpdateOn= DateTime.Now;
                tbl.UpdateBy= book.UpdateBy;
                _context.Add(tbl);
                _context.SaveChanges();
                response.Status = true;
                
            }
            catch (Exception ex)
            {
                response.Status= false;
            }
            return response;
        }
        public ResponseDTO GetOneBook(int id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {

                var book = _context.TblBookMsts.Where(x => x.BookId == id && x.IsDeleted == false).FirstOrDefault();
                if (book != null)
                {
                    BookViewDTO bookDto = new BookViewDTO
                    {
                        BookId = book.BookId,
                        BookName = book.BookName,
                        AuthorName = book.AuthorName,
                        Publisher = book.Publisher,
                        Price = book.Price,
                        Description=book.Description,
                        Pdfpath = book.Pdfpath,
                        CoverImagePath = book.CoverImagePath,
                    };
                    response.Data = bookDto;
                    response.Message = "Success";
                    response.Status = true;
                }
                else
                {
                    response.Data = null;
                    response.Message = "Details not Found...";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
                response.Status = false;
            }
            return response;
        }
        public ResponseDTO DownloadReport()
        {
            ResponseDTO response=new ResponseDTO();
            try
            {

                var list = new TblBookDownloadMst();
                var Dcount = 0;
                List<TblBookMst> BookList = new List<TblBookMst>();
                BookList = _context.TblBookMsts.ToList();
                if (BookList != null)
                {
                  
                    
                    //List<ReportDTO> RepoList = BookList.Select(p => new ReportDTO
                    //{
                    //    RecordId=p.BookId,
                    //    BookName = p.BookName,
                    //    AuthorName = p.AuthorName,
                    //    Edition = p.Edition,
                    //    Category = p.CategoryId,
                    //    SubCategory = p.SubCategoryId,
                    //    BookPages = p.BookPages,
                    //    DownloadCount = Dcount
                    //}).Where(x=>x.DownloadCount>0).ToList();

                    //List<ReportDTO> repoDto = new List<ReportDTO>();

                    //repoDto = RepoList.Where(x => x.IsDeleted == false).ToList();
                    List<ReportDTO> dtoList = (from r in BookList
                                               join
                                                        c in _context.TblCategoryMsts on r.CategoryId equals c.CategoryId
                                               join
                                              s in _context.TblSubCategoryMsts on r.SubCategoryId equals s.SubCategoryId


                                               select new {r, c, s }).ToList().Select(x => new ReportDTO
                                                        {
                                                            SubCategory = x.s.SubCategoryId,
                                                            Category = x.c.CategoryId,
                                                            CategoryName = x.c.CategoryName,
                                                            SubCategoryName = x.s.SubCategoryName,
                                                           RecordId = x.r.BookId,
                                                           BookName = x.r.BookName,
                                                           AuthorName = x.r.AuthorName,
                                                           Edition = x.r.Edition,
                                                           CoverImagePath = x.r.CoverImagePath,
                                                           CreatedOn = x.r.CreatedOn,
                                                           BookPages = x.r.BookPages,
                                                           DownloadCount = Dcount

                                               }).ToList();

                      foreach (var item in dtoList)
                    {
                        
                        item.DownloadCount = _context.TblBookDownloadMsts.Where(x => x.BookId == item.RecordId).Count();

                    }
                    List<ReportDTO> repoDto = new List<ReportDTO>();
                    repoDto=dtoList.Where(x=>x.DownloadCount>0).ToList();



                    response.Data = repoDto;
                    response.Status = true;
                    response.Message = "Success";
                }
                else
                {
                    response.Data = null;
                    response.Status = false;
                    response.Message = "fail";
                }
            }
            catch (Exception ex)
            {
                response.Message=ex.Message;
                response.Data = ex;
                response.Status=false;
            }
            return response;
        }
        public ResponseDTO GetAllBookById(int catid, int subcatid)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {

                List<TblBookMst> book = new List<TblBookMst>();
                var books = _context.TblBookMsts.Where(x => x.IsDeleted == false).AsQueryable();
                if(catid > 0)
                {
                    books = books.Where(x => x.CategoryId == catid);
                }
                if (subcatid > 0)
                {
                    books = books.Where(x => x.SubCategoryId == subcatid);
                }
                book = books.ToList();
                //book = _context.TblBookMsts.Where(x => x.SubCategoryId == id && x.IsDeleted == false).ToList();
                if (book != null)
                {
                    List<BookViewDTO> bookDto =book.Select(x=>new BookViewDTO
                    {
                        BookId = x.BookId,
                        BookName = x.BookName,
                        AuthorName = x.AuthorName,
                        Publisher = x.Publisher,
                        Price = x.Price,
                        CoverImagePath = x.CoverImagePath,
                    }).ToList();

                    response.Data = bookDto;
                    response.Message = "Success";
                    response.Status = true;
                }
                else
                {
                    response.Data = string.Empty;
                    response.Message = "Details not Found...";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
                response.Status = false;
            }
            return response;
        }
        public ResponseDTO GetAllBooks()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {

                var list = new TblBookDownloadMst();
                var Dcount = 0;
                List<TblBookMst> BookList = new List<TblBookMst>();
                DateTime endDate=DateTime.Now;
                DateTime startDate = endDate.AddDays(-7);
                BookList = _context.TblBookMsts.Where(x => x.IsDeleted == false && (x.CreatedOn<=endDate && x.CreatedOn>=startDate)).ToList();
                if (BookList != null)
                {


                    //List<ReportDTO> RepoList = BookList.Select(p => new ReportDTO
                    //{
                    //    RecordId=p.BookId,
                    //    BookName = p.BookName,
                    //    AuthorName = p.AuthorName,
                    //    Edition = p.Edition,
                    //    Category = p.CategoryId,
                    //    SubCategory = p.SubCategoryId,
                    //    BookPages = p.BookPages,
                    //    DownloadCount = Dcount
                    //}).Where(x=>x.DownloadCount>0).ToList();

                    //List<ReportDTO> repoDto = new List<ReportDTO>();

                    //repoDto = RepoList.Where(x => x.IsDeleted == false).ToList();
                    List<ReportDTO> dtoList = (from r in BookList
                                               join
                                                        c in _context.TblCategoryMsts on r.CategoryId equals c.CategoryId
                                               join
                                              s in _context.TblSubCategoryMsts on r.SubCategoryId equals s.SubCategoryId


                                               select new { r, c, s }).ToList().Select(x => new ReportDTO
                                               {
                                                   SubCategory = x.s.SubCategoryId,
                                                   Category = x.c.CategoryId,
                                                   CategoryName = x.c.CategoryName,
                                                   SubCategoryName = x.s.SubCategoryName,
                                                   RecordId = x.r.BookId,
                                                   BookName = x.r.BookName,
                                                   AuthorName = x.r.AuthorName,
                                                   Edition = x.r.Edition,
                                                   CoverImagePath = x.r.CoverImagePath,
                                                   CreatedOn = x.r.CreatedOn,
                                                   BookPages = x.r.BookPages,
                                                   DownloadCount = Dcount

                                               }).OrderByDescending(x=>x.CreatedOn).ToList();

                    foreach (var item in dtoList)
                    {

                        item.DownloadCount = _context.TblBookDownloadMsts.Where(x => x.BookId == item.RecordId).Count();

                    }
                    //List<ReportDTO> repoDto = new List<ReportDTO>();
                    //repoDto = dtoList.Where(x => x.DownloadCount > 0).ToList();



                    response.Data = dtoList;
                    response.Status = true;
                    response.Message = "Success";
                }
                else
                {
                    response.Data = null;
                    response.Status = false;
                    response.Message = "fail";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
                response.Status = false;
            }
            return response;
        }
    }
}
