namespace ProductPackaging.DTOs
{
    public class ProductV1ResponseDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = default!;
    }

    public class CreateProductDto
    {
        public string ProductName { get; set; } = default!;
    }
}