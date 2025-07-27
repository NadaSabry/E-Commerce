namespace E_Commerce.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CustomerName { get; set; }
        public int Status { get; set; } // 1: Init, 2: Confirmed
        public decimal TotalAmount { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    }
}
