namespace Project_BTG_Pactual_Api.Applicacao.DTO
{
    using System;

    public class ClientOrderDto
    {
        public int OrderId { get; set; }    // <--- ID da tabela Order
        public int ClienteId { get; set; }
        public int CodigoPedido { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }


    public class OrderItemDto
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalItem { get; set; }

    }
}
