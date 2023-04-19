using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface ICategory
    {
        public ResponseDTO GetCategory();
        public ResponseDTO CreateCategory(CategoryDTO category);
        public ResponseDTO UpdateCategory(int id);
        public ResponseDTO UpdateCategory(CategoryDTO model);
        public ResponseDTO DeleteCategory(int id);
    }
}
