using BusinessObjects;
using DataAccessLayer;
using System.Collections.Generic;


namespace Repositories
{
    public class ProductRepository: IProductRepository
    {
        public void SaveProduct(Product p)
        {
            ProductDAO.SaveProduct(p);
        }

        public void DeleteProduct(Product p)
        {
            
            ProductDAO.DeleteProduct(p.ProductID);
        }

        public void UpdateProduct(Product p)
        {
            ProductDAO.UpdateProduct(p);
        }

        public List<Product> GetProducts()
        {
            // Call the static method directly on the type, not on an instance
            return ProductDAO.GetProducts();
        }

        public Product GetProductByID(int id)
        {
            return ProductDAO.GetProductByID(id);
        }


    }
}
