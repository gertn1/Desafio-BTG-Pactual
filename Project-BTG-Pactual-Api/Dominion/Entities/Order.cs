namespace Project_BTG_Pactual_Api.Dominion.Entities
{

    public class Order
    {
        public int Id { get; set; }
        public int CodigoPedido { get; set; }
        public int ClientId { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }


    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Order Order { get; set; }
    }

}
