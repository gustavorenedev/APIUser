using ProjectFor7COMm.Models;

namespace ProjectFor7COMm.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task RegisterUser(User user, string password);
        Task<bool> ValidateUser(string username, string password);
        Task<bool> ResetPassword(string email, string newPassword);
        Task<bool> DeleteUser(int id);
    }
}
