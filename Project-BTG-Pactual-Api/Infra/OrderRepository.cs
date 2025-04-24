namespace OrderMS.Infra.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Project_BTG_Pactual_Api.Dominion.Entities;
    using Project_BTG_Pactual_Api.Dominion.InterfacesRepositores;
    using Project_BTG_Pactual_Api.Infra.Data;

    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _db;
        public OrderRepository(OrderDbContext db) => _db = db;

        public async Task AddAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }

        public Task<decimal> GetTotalValueAsync(int orderId) =>
            _db.Orders
               .Where(o => o.CodigoPedido == orderId)
               .Select(o => o.TotalValue)
               .FirstOrDefaultAsync();

        public Task<IReadOnlyList<Order>> GetByClientAsync(int clientId) =>
            _db.Orders
               .Where(o => o.ClientId == clientId)
               .ToListAsync()
               .ContinueWith(t => (IReadOnlyList<Order>)t.Result);

        public Task<int> GetCountByClientAsync(int clientId) =>
            _db.Orders.CountAsync(o => o.ClientId == clientId);
    }
}
