using BusinessLayer;
using DTOs;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImpl
{
    public class BookDownloadImpl : IBookDownload
    {
        private readonly BookDownloadBLL _book;
        public BookDownloadImpl(BookDownloadBLL book)
        {
            this._book = book;
        }
        public ResponseDTO AddDownload(BookDownloadDTO downloadDTO)
        {
            return _book.DownloaderDetails(downloadDTO);
            //throw new NotImplementedException();
        }

        public ResponseDTO AllBooks()
        {
            return _book.GetAllBooks();
            throw new NotImplementedException();
        }

        public ResponseDTO GetBookById(int id)
        {
            return _book.GetOneBook(id);
            //throw new NotImplementedException();
        }

        public ResponseDTO GetBooksById(int catid, int subcatid)
        {
            return _book.GetAllBookById(catid, subcatid);
            //throw new NotImplementedException();
        }

        public ResponseDTO Report()
        {
            return _book.DownloadReport();
            //throw new NotImplementedException();
        }
    }
}
