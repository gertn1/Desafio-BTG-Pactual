namespace Project_BTG_Pactual_Api.Applicacao.DTO
{
    using System;

    public class ClientOrderDto
    {
        public int CodigoPedido { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
