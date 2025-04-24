namespace OrderMS.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Project_BTG_Pactual_Api.Applicacao.DTO;
    using Project_BTG_Pactual_Api.Applicacao.interfacesServices;
    using Project_BTG_Pactual_Api.Dominion.Entities;
    using Project_BTG_Pactual_Api.Dominion.InterfacesRepositores;
    using Project_BTG_Pactual_Api.Messages;

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        public OrderService(IOrderRepository repo) => _repo = repo;

        public async Task ProcessOrderAsync(OrderMessage msg)
        {
            var order = new Order
            {
                CodigoPedido = msg.CodigoPedido,
                ClientId = msg.CodigoCliente,
                CreatedAt = DateTime.UtcNow,
                Items = msg.Itens.Select(i => new OrderItem
                {
                    Product = i.Produto,
                    Quantity = i.Quantidade,
                    UnitPrice = i.Preco
                }).ToList()
            };

            // cálculo de total fica aqui
            order.TotalValue = order.Items.Sum(x => x.Quantity * x.UnitPrice);

            await _repo.AddAsync(order);
        }

        public Task<decimal> GetTotalAsync(int orderId) =>
            _repo.GetTotalValueAsync(orderId);

        public async Task<IReadOnlyList<ClientOrderDto>> GetByClientAsync(int clientId)
        {
            var orders = await _repo.GetByClientAsync(clientId);
            return orders
                .Select(o => new ClientOrderDto
                {
                    CodigoPedido = o.CodigoPedido,
                    TotalValue = o.TotalValue,
                    CreatedAt = o.CreatedAt
                })
                .ToList();
        }

        public Task<int> GetCountAsync(int clientId) =>
            _repo.GetCountByClientAsync(clientId);
    }
}
