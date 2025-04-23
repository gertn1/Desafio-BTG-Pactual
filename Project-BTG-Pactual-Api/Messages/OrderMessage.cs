namespace Project_BTG_Pactual_Api.Messages
{
    public class OrderMessage
    {
        public int CodigoPedido { get; set; }
        public int CodigoCliente { get; set; }
        public List<OrderItemMessage> Itens { get; set; }
    }

    public class OrderItemMessage
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
