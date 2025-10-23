namespace BusinessObjects
{
    public partial class Category
    {
        //Icolection biểu diễn mối quan hệ một-nhiều giữa Category và Product
        //virtual để hỗ trợ lazy loading nếu sử dụng Entity Framework
        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public Category(int categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
        }
    }
}