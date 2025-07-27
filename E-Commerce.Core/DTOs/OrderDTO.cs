using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTOs
{
    public class OrderDTO
    {
        public string? CustomerName { get; set; }
        public List<OrderItemDTO> orderItemDTOs { get; set; } = new List<OrderItemDTO>();
    }
}
