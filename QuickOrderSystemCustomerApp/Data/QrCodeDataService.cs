namespace QuickOrderSystemCustomerApp.Data
{
    public class QrCodeDataService
    {
        public static int? UserId { get; private set; }
        public static int? TableNumber { get; private set; }

        public void SetUserId(int userId, int tableNumber)
        {
            UserId = userId;
            TableNumber = tableNumber;
        }

        public void ClearUserId()
        {
            UserId = null;
            TableNumber = null;
        }
    }
}
