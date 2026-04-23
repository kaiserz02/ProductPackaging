namespace ProductPackaging.Entities
{
    public class PackagingType
    {
        public int PackageTypeId { get; set; }
        public string PackageTypeName { get; set; } = default!;

        public List<Packaging> Packages { get; set; } = new();
    }
}
