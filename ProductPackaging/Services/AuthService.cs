using ProductPackaging.Data;
using ProductPackaging.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductPackaging.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            AppDbContext db,
            TokenService tokenService,
            ILogger<AuthService> logger)
        {
            _db = db;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            _logger.LogInformation("Attempting to register user: {Username}", username);

            var exists = await _db.Users.AnyAsync(x => x.Username == username);

            if (exists)
            {
                _logger.LogWarning("Registration failed - username already exists: {Username}", username);
                return false;
            }

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            _logger.LogInformation("User registered successfully: {Username}", username);

            return true;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            _logger.LogInformation("Login attempt for user: {Username}", username);

            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                _logger.LogWarning("Login failed - user not found: {Username}", username);
                return null;
            }

            var valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!valid)
            {
                _logger.LogWarning("Login failed - invalid password: {Username}", username);
                return null;
            }

            var token = _tokenService.CreateToken(user);

            _logger.LogInformation("Login successful: {Username}", username);

            return token;
        }
    }
}
