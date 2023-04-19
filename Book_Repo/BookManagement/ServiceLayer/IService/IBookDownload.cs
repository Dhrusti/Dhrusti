using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IBookDownload
    {
        public ResponseDTO AddDownload(BookDownloadDTO BDmodel);
        public ResponseDTO GetBookById(int id);
        public ResponseDTO Report();
        public ResponseDTO AllBooks();
        public ResponseDTO GetBooksById(int catid, int subcatid);
    }
}
