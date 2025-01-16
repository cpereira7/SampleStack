using SampleStack.AutoMapper.Models;

namespace SampleStack.AutoMapper.Services
{
    internal class DataRetrievalFacade
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly CustomerService _customerService;

        public DataRetrievalFacade(OrderService orderService, ProductService productService, CustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task RetrieveAllDataAsync()
        {
            await _productService.RetrieveData();
            await _customerService.RetrieveData();
            await _orderService.RetrieveData();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderService.GetAllItems();
        }
    }

}
