using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IBook
    {
        public ResponseDTO GetBook();
        public ResponseDTO CreateBook(BookDTO book);
        public ResponseDTO GetCategory();
        public ResponseDTO GetSubCategory(int? id);
        public ResponseDTO DeleteBook(int id);
        public ResponseDTO Editbook(int id);
        public ResponseDTO Editbooks(BookViewDTO book);







    }
}
