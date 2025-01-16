namespace SampleStack.AutoMapper.Models
{
    internal class BaseModel
    {
        public int Id { get; set; }
    }

    internal class Customer : BaseModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    internal class Order : BaseModel
    {
        public DateTime OrderDate { get; set; }
        public required Customer Customer { get; set; }
        public required List<OrderItem> Items { get; set; } = [];
        public decimal TotalAmount => Items.Sum(item => item.TotalAmount);
    }

    internal class OrderItem : BaseModel
    {
        public int OrderId { get; set; }
        public required Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal TotalAmount => Product.Price * Quantity;
    }

    internal class Product : BaseModel
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
    }
}
