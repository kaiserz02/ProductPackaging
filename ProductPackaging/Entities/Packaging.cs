namespace ProductPackaging.Entities
{
    public class Packaging
    {
        public int PackageId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int PackageTypeId { get; set; }
        public PackagingType PackageType { get; set; } = default!;

        public int? ParentPackageId { get; set; }
        public Packaging? Parent { get; set; }

        public List<Packaging> Children { get; set; } = new();

        public List<PackagingItem> PackagingItems { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
