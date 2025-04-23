using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderMS.Data;

namespace OrderMS.Controllers;
[ApiController]
[Route("api")]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _db;
    public OrdersController(OrderDbContext db) => _db = db;

    [HttpGet("orders/{orderId}/total")]
    public async Task<IActionResult> GetTotal(int orderId)
    {
        var total = await _db.Orders
            .Where(o => o.CodigoPedido == orderId)
            .Select(o => o.TotalValue)
            .FirstOrDefaultAsync();
        return Ok(new { orderId, total });
    }

    [HttpGet("clients/{clientId}/orders")]
    public async Task<IActionResult> GetByClient(int clientId)
    {
        var list = await _db.Orders
            .Where(o => o.ClientId == clientId)
            .Select(o => new { o.CodigoPedido, o.TotalValue, o.CreatedAt })
            .ToListAsync();
        return Ok(list);
    }

    [HttpGet("clients/{clientId}/orders/count")]
    public async Task<IActionResult> GetCount(int clientId)
    {
        var count = await _db.Orders.CountAsync(o => o.ClientId == clientId);
        return Ok(new { clientId, count });
    }
}
