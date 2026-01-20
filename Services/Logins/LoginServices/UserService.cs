using CommercialManagement.Models.Users;

namespace CommercialManagement.Services.Logins.LoginServices
{
    public interface UserService
    {
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        User GetUserByUsername(string username);
        List<User> GetAllUsers();
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        bool EmailExists(string email);
    }
}
