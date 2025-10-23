using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProductManagementDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer;

public class ProductDAO
{
    public static List<Product> GetProducts()
    {
        var listProducts = new List<Product>();
        try
        {
            using var context = new MyStoreContext();
            listProducts = context.Products.Include(p => p.Category).ToList();
        } catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return listProducts;
        ;
    }
    


    public static void SaveProduct(Product p)
    {
        try
        {
            using var context = new MyStoreContext();
            context.Products.Add(p);
            context.SaveChanges();
        }
        catch (Exception ex) 
        {
            throw new Exception(ex.Message);
        }
    }

    public static void UpdateProduct(Product product)
    {
        try
        {
            using var context = new MyStoreContext();

            // Tìm sản phẩm hiện có trong DB
            var existing = context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existing == null)
            {
                throw new Exception("Product not found");
            }

            // Cập nhật từng thuộc tính
            existing.ProductName = product.ProductName;
            existing.UnitPrice = product.UnitPrice;
            existing.UnitsInStock = product.UnitsInStock;
            existing.CategoryID = product.CategoryID;

            // Lưu thay đổi
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Update failed: {ex.Message}");
        }
    }


    public static void DeleteProduct(int productID)
    {
        try
        {
            using var context = new MyStoreContext();
            var p1 = context.Products.SingleOrDefault(p => p.ProductID == productID);
            context.Products.Remove(p1);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Product GetProductByID(int id)
    {
        using var context = new MyStoreContext();
        return context.Products.SingleOrDefault(p => p.ProductID == id);
    }

}



