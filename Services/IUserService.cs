public interface IUserService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<User> RegisterAsync(string email, string password);
    Task<bool> DeleteUserAsync(int userId);
}
