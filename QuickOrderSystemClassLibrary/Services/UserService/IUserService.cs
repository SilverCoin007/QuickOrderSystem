using System;
using System.Threading.Tasks;
using QuickOrderSystemClassLibrary.Enum;

namespace QuickOrderSystemClassLibrary.Services.UserService
{
    public interface IUserService
    {
        Task<User?> RegisterUser(User? user);
        Task<User?> GetUserByUsername(string username);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}