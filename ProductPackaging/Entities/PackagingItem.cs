namespace ProductPackaging.Entities
{
    public class PackagingItem
    {
        public int PackageId { get; set; }
        public Packaging Package { get; set; } = default!;

        public int ItemId { get; set; }
        public Item Item { get; set; } = default!;
    }
}
