namespace BusinessObjects
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public Product(int productID, string productName, decimal? unitPrice, short? unitsInStock, int? categoryID)
        {
            ProductID = productID;
            ProductName = productName;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
            CategoryID = categoryID;
        }

        public Product() { }
    }
}