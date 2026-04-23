using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductPackaging.Data;
using ProductPackaging.Entities;

namespace ProductPackaging.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;
        private readonly PasswordHasher<User> _hasher;

        public AuthService(AppDbContext db, TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
            _hasher = new PasswordHasher<User>();
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var exists = await _db.Users.AnyAsync(x => x.Username == username);
            if (exists) return false;

            var user = new User
            {
                Username = username
            };

            user.PasswordHash = _hasher.HashPassword(user, password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return _tokenService.CreateToken(user);
        }
    }
}
