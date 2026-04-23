namespace ProductPackaging.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;

        public List<Packaging> Packages { get; set; } = new();
    }
}
