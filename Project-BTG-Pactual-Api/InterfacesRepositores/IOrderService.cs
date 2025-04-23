namespace OrderMS.Infra.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project_BTG_Pactual_Api.Entities;

    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<decimal> GetTotalValueAsync(int orderId);
        Task<IReadOnlyList<Order>> GetByClientAsync(int clientId);
        Task<int> GetCountByClientAsync(int clientId);
    }
}
