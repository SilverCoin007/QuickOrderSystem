namespace QuickOrderSystemClassLibrary.Services
{
    public class UserIdService
    {
        public static int? UserId { get; private set; }

        public void SetUserId(int userId)
        {
            UserId = userId;
        }

        public void ClearUserId()
        {
            UserId = null;
        }
    }
}
