using SampleStack.AutoMapper.DTOs;

namespace SampleStack.AutoMapper.Data
{
    internal class ProductDataSource : BaseDataSource<ProductDto>
    {
        public ProductDataSource()
        {
            Data.Add(new ProductDto { Id = 1, Name = "Laptop", Price = 999.99m });
            Data.Add(new ProductDto { Id = 2, Name = "Phone", Price = 699.99m });
            Data.Add(new ProductDto { Id = 3, Name = "Headphones", Price = 199.99m });
        }
    }

    internal class CustomerDataSource : BaseDataSource<CustomerDto>
    {
        public CustomerDataSource()
        {
            Data.Add(new CustomerDto { Id = 1, Name = "Alice", Email = "alice@example.com" });
            Data.Add(new CustomerDto { Id = 2, Name = "Bob", Email = "bob@example.com" });
        }
    }

    internal class OrderDataSource : BaseDataSource<OrderDto>
    {
        public OrderDataSource()
        {
            Data.Add(new OrderDto
            {
                Id = 1,
                OrderDate = DateTime.UtcNow.AddDays(-1),
                CustomerId = 1,
                Items =
                [
                    new OrderItemDto { Id = 1, ProductId = 1, Quantity = 1 },
                    new OrderItemDto { Id = 2, ProductId = 2, Quantity = 2 }
                ]
            });

            Data.Add(new OrderDto
            {
                Id = 2,
                OrderDate = DateTime.UtcNow.AddDays(-2),
                CustomerId = 2,
                Items =
                [
                    new OrderItemDto { Id = 1, ProductId = 3, Quantity = 2 },
                ]
            });
        }
    }
}
