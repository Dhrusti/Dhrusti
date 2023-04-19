using BusinessLayer;
using BussinessLayer;
using DTOs;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImpl
{
    public class BookImpl : IBook
    {
        private readonly BookBLL _BookBLL;
        public BookImpl(BookBLL BookBLLL)
        {
            this._BookBLL = BookBLLL;
        }
        public ResponseDTO GetBook()
        {
            return _BookBLL.GetBook();
        }
        public ResponseDTO CreateBook(BookDTO book)
        {
            return _BookBLL.CreateBook(book);
        }
        public ResponseDTO GetCategory()
        {
            return _BookBLL.GetCategory();
        }
        public ResponseDTO GetSubCategory(int? id)
        {
            return _BookBLL.GetSubCategory(id);
        }
        public ResponseDTO DeleteBook(int id)
        {
            return _BookBLL.DeleteBook(id);
        }
        public ResponseDTO Editbook(int id)
        {
            return _BookBLL.Editbook(id);
        }
        public ResponseDTO Editbooks(BookViewDTO book)
        {
            return _BookBLL.Editbook(book);
        }
    }
}
