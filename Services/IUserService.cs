public interface IUserService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<User> RegisterAsync(string email, string password, string role);
    Task<bool> DeleteUserAsync(int userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
}
