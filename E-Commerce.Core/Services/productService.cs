using E_Commerce.Core.Interface;
using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
    public class productService
    {
        private readonly IBaseRepository<Product> _repository;
        private readonly IBaseRepository<OrderDetails> _orderDetailsRepository;

        public productService(IBaseRepository<Product> repository, IBaseRepository<OrderDetails> orderDetailsRepository)
        {
            _repository = repository;
            _orderDetailsRepository = orderDetailsRepository;
        }
        public async ValueTask<Product?> GetProductByID(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async ValueTask<List<OrderDetails>?> GetProductByOrderID(int orderId)
        {
            var x = await _orderDetailsRepository.GetWhereIncludeAsync(x => x.OrderId == orderId, x=>x.product);
            return x;
        }
        public async Task<List<Product>> GetAllProduct()
        {
            return await _repository.GetAllAsync(); 
        }
        public void AddProduct(Product product)
        {
            _repository.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
        }
        public async void DeleteProduct(Product product)
        {
            _repository.Delete(product);
        }
    }
}
