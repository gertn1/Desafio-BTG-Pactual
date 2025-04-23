using MassTransit;
using OrderMS.Data;
using Project_BTG_Pactual_Api.Entities;
using Project_BTG_Pactual_Api.Messages;

namespace OrderMS.Consumers;
public class OrderConsumer : IConsumer<OrderMessage>
{
    private readonly OrderDbContext _db;
    public OrderConsumer(OrderDbContext db) => _db = db;

    public async Task Consume(ConsumeContext<OrderMessage> context)
    {
        var msg = context.Message;
        var order = new Order
        {
            CodigoPedido = msg.CodigoPedido,
            ClientId = msg.CodigoCliente,
            CreatedAt = DateTime.UtcNow
        };

        order.Items = msg.Itens.Select(i => new OrderItem
        {
            Product = i.Produto,
            Quantity = i.Quantidade,
            UnitPrice = i.Preco
        }).ToList();

        order.TotalValue = order.Items.Sum(x => x.Quantity * x.UnitPrice);

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
    }
}
