namespace ProductPackaging.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = default!;

        public List<PackagingItem> PackagingItems { get; set; } = new();
    }
}
