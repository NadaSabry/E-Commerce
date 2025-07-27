using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTOs.orderResponse
{
    public class OrderItemResponseDto
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? productImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
