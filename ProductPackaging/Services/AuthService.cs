using ProductPackaging.Data;
using ProductPackaging.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductPackaging.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;

        public AuthService(AppDbContext db, TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var exists = await _db.Users.AnyAsync(x => x.Username == username);
            if (exists) return false;

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user == null) return null;

            var valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!valid) return null;

            return _tokenService.CreateToken(user);
        }
    }
}
