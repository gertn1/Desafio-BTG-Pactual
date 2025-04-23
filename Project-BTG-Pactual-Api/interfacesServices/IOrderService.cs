namespace OrderMS.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OrderMS.Services.DTOs;
    using Project_BTG_Pactual_Api.Messages;

    public interface IOrderService
    {
        Task ProcessOrderAsync(OrderMessage msg);
        Task<decimal> GetTotalAsync(int orderId);
        Task<IReadOnlyList<ClientOrderDto>> GetByClientAsync(int clientId);
        Task<int> GetCountAsync(int clientId);
    }
}
