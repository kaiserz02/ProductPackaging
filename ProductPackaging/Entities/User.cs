namespace ProductPackaging.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
    }
}
