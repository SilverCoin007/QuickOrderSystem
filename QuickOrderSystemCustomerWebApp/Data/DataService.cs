using QuickOrderSystemClassLibrary;

namespace QuickOrderSystemCustomerWebApp.Data
{
    public class DataService
    {
        public static int? UserId { get; private set; }
        public static int? TableNumber { get; private set; }
        public List<Product>? Product { get; private set; }
        public List<Category>? Category { get; private set; }

        public void SetUserId(int userId, int tableNumber, List<Product>? products, List<Category>? category)
        {
            UserId = userId;
            TableNumber = tableNumber;
            Product = products;
            Category = category;

        }

        public void ClearUserId()
        {
            UserId = null;
            TableNumber = null;
            Product = null;
            Category = null;
        }
    }
}
