namespace ProductPackaging.DTOs
{
    public class ProductV2ResponseDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = default!;

        public List<PackageDto> Packages { get; set; } = new();
    }

    public class PackageDto
    {
        public int PackageID { get; set; }
        public int PackageTypeID { get; set; }
        public string PackageTypeName { get; set; } = default!;
        public int? ParentID { get; set; }

        public List<ItemDto> Items { get; set; } = new();

        public List<PackageDto> Packages { get; set; } = new();
    }

    public class ItemDto
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; } = default!;
    }
}