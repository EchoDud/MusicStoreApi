using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;


public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null || !VerifyPassword(password, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<User> RegisterAsync(string email, string password, string role)
    {
        if (await _context.Users.AnyAsync(u => u.Email == email))
            throw new InvalidOperationException("User with this email already exists.");

        var user = new User
        {
            Email = email,
            PasswordHash = HashPassword(password),
            Role = role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private bool VerifyPassword(string password, string hash)
    {
        using var sha256 = SHA256.Create();
        var computedHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return computedHash == hash;
    }
}
