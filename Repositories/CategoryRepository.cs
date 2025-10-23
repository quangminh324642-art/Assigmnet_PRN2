using BusinessObjects;      

using System.Collections.Generic;
using DataAccessLayer;

namespace Repositories
{
    public class CategoryRepository: ICatergoryRepository
    {
        public List<Category> GetCategories()
        {
            return CategoryDAO.GetCategories();
        }
    }
}
