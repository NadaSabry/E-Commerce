using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTOs.orderResponse
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CustomerName { get; set; }
        public int Status { get; set; } // 1: Init, 2: Confirmed
        public decimal TotalAmount { get; set; }

        public List<OrderItemResponseDto> OrderDetails { get; set; }  = new List<OrderItemResponseDto>();
    }
}
