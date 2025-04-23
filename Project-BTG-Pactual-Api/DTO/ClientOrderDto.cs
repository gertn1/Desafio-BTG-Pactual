namespace OrderMS.Services.DTOs
{
    using System;

    public class ClientOrderDto
    {
        public int CodigoPedido { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
