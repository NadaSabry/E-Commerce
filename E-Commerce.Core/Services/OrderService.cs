using E_Commerce.Core.DTOs;
using E_Commerce.Core.DTOs.orderResponse;
using E_Commerce.Core.Interface;
using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
    public class OrderService
    {
        private readonly IBaseRepository<Order> _repository;
        private readonly IBaseRepository<Product> _productRepository;
        private readonly productService _productService;

        public OrderService(IBaseRepository<Order> repository, IBaseRepository<Product> productRepository, productService productService)
        {
            _repository = repository;
            _productRepository = productRepository;
            _productService = productService;
        }
        public async ValueTask<OrderResponseDTO?> GetOrderByID(int id)
        {
            var orderData = await _repository.GetByIdAsync(id);
            if (orderData == null)
                return null;

            var orderDetails = await _productService.GetProductByOrderID(id);
            var result = new OrderResponseDTO()
            {
                CreationDate = orderData.CreationDate,
                CustomerName = orderData.CustomerName,
                Status = orderData.Status,
                Id = orderData.Id,
                TotalAmount = orderData.TotalAmount,
                OrderDetails = orderData.OrderDetails.Select(item => new OrderItemResponseDto()
                {
                    ProductId = item.ProductId,
                    ProductName = item.product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.product.Price,
                    productImageUrl = item.product.ImageUrl 
                }).ToList()
            };
            return result;
        }
        public async Task<List<Order>> GetAllOrder()
        {
            var ordersWithDetails = await _repository.GetAllAsync(x=>x.OrderDetails);
            return ordersWithDetails;
        }
        public async Task<int> AddOrder(OrderDTO orderDTO)
        {
            Order order = new Order()
            {
                CreationDate = DateTime.Now,
                CustomerName = orderDTO.CustomerName,
                Status = 1, // Assuming 1 is the initial status
            };
            decimal totalAmount = 0;
            foreach (var item in orderDTO.orderItemDTOs)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} not found.");
                }
                order.OrderDetails.Add(new OrderDetails()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
                totalAmount += product.Price * item.Quantity;
            }
            order.TotalAmount = totalAmount;
            _repository.Add(order);
            return order.Id;
        }
        public void UpdateOrder(Order order)
        {
            _repository.Update(order);
        }
        public async void DeleteOrder(Order order)
        {
            _repository.Delete(order);
        }

        public async ValueTask<OrderResponseDTO?> ConfirmOrder(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return null;

            order.Status = 2; // Assuming 2 is the confirmed status
            _repository.Update(order);

            var orderDetails = await _productService.GetProductByOrderID(id);
            var result = new OrderResponseDTO()
            {
                CreationDate = order.CreationDate,
                CustomerName = order.CustomerName,
                Status = order.Status,
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                OrderDetails = order.OrderDetails.Select(item => new OrderItemResponseDto()
                {
                    ProductId = item.ProductId,
                    ProductName = item.product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.product.Price,
                }).ToList()
            };
            return result;
        }
    }
}
