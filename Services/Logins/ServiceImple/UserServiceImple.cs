using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Users;
using CommercialManagement.Services.Logins.LoginServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.ServiceImple
{
    public class UserServiceImple : UserService
    {
        private readonly CommercialDBContext _context;
        private readonly ILogger<UserServiceImple> _logger;

        public UserServiceImple(CommercialDBContext context, ILogger<UserServiceImple> logger)
        {
            _context = context;
            _logger = logger;
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                var user = _context.User
                    .FirstOrDefault(u => u.UserName == username);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by username: {Username}", username);
                throw;
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                var user = _context.User
                    .FirstOrDefault(u => u.UserID == userId);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by ID: {UserId}", userId);
                throw;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                // TODO: Create stored procedure: usp_GetAllUsers
                // Parameters: None
                // Returns: All user records

                var users = _context.User
                    .FromSqlRaw("EXEC usp_GetAllUsers")
                    .ToList();

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                throw;
            }
        }

        public bool AddUser(User user)
        {
            try
            {
                // TODO: Create stored procedure: usp_AddUser
                // Parameters: @UserName, @Upassword, @FullName, @Email
                // Returns: Number of affected rows

                var parameters = new[]
                {
                    new SqlParameter("@UserName", user.UserName ?? (object)DBNull.Value),
                    new SqlParameter("@Upassword", user.Upassword ?? (object)DBNull.Value),
                    new SqlParameter("@FullName", user.FullName ?? (object)DBNull.Value),
                    new SqlParameter("@Email", user.Email ?? (object)DBNull.Value)
                };

                var result = _context.Database.ExecuteSqlRaw(
                    "EXEC usp_AddUser @UserName, @Upassword, @FullName, @Email",
                    parameters);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user: {Username}", user.UserName);
                throw;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                // TODO: Create stored procedure: usp_UpdateUser
                // Parameters: @UserID, @UserName, @Upassword, @FullName, @Email
                // Returns: Number of affected rows

                var parameters = new[]
                {
                    new SqlParameter("@UserID", user.UserID),
                    new SqlParameter("@UserName", user.UserName ?? (object)DBNull.Value),
                    new SqlParameter("@Upassword", user.Upassword ?? (object)DBNull.Value),
                    new SqlParameter("@FullName", user.FullName ?? (object)DBNull.Value),
                    new SqlParameter("@Email", user.Email ?? (object)DBNull.Value)
                };

                var result = _context.Database.ExecuteSqlRaw(
                    "EXEC usp_UpdateUser @UserID, @UserName, @Upassword, @FullName, @Email",
                    parameters);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user: {UserId}", user.UserID);
                throw;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                // TODO: Create stored procedure: usp_DeleteUser
                // Parameters: @UserID INT
                // Returns: Number of affected rows

                var param = new SqlParameter("@UserID", userId);
                var result = _context.Database.ExecuteSqlRaw(
                    "EXEC usp_DeleteUser @UserID",
                    param);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user: {UserId}", userId);
                throw;
            }
        }

        public bool EmailExists(string email)
        {
            try
            {
                // TODO: Create stored procedure: usp_CheckEmailExists
                // Parameters: @Email VARCHAR(100)
                // Returns: COUNT(*)

                var param = new SqlParameter("@Email", email);
                var outputParam = new SqlParameter("@Exists", System.Data.SqlDbType.Bit)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                _context.Database.ExecuteSqlRaw(
                    "EXEC usp_CheckEmailExists @Email, @Exists OUTPUT",
                    param, outputParam);

                return (bool)outputParam.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if email exists: {Email}", email);
                throw;
            }
        }
        public User GetUserByEmail(string email)
        {
            try
            {
                return _context.User.FirstOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email: {Email}", email);
                throw;
            }
        }
    }
}