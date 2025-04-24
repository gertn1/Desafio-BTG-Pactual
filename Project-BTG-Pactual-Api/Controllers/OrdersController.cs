using Microsoft.AspNetCore.Mvc;
using Project_BTG_Pactual_Api.Applicacao.interfacesServices;

namespace OrderMS.Controllers
{
    [ApiController]
    [Route("api")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService service) => _service = service;

        [HttpGet("orders/{orderId}/total")]
        public async Task<IActionResult> GetTotal(int orderId)
        {
            var total = await _service.GetTotalAsync(orderId);
            return Ok(new { orderId, total });
        }

        [HttpGet("clients/{clientId}/orders")]
        public async Task<IActionResult> GetByClient(int clientId)
        {
            var list = await _service.GetByClientAsync(clientId);
            return Ok(list);
        }

        [HttpGet("clients/{clientId}/orders/count")]
        public async Task<IActionResult> GetCount(int clientId)
        {
            var count = await _service.GetCountAsync(clientId);
            return Ok(new { clientId, count });
        }
    }
}
