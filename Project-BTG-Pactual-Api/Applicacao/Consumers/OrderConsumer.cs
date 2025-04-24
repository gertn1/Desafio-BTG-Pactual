
using MassTransit;
using Project_BTG_Pactual_Api.Messages;
using Project_BTG_Pactual_Api.Applicacao.interfacesServices;

namespace Project_BTG_Pactual_Api.Applicacao.Consumers
{
    public class OrderConsumer : IConsumer<OrderMessage>
    {
        private readonly IOrderService _service;
        public OrderConsumer(IOrderService service) => _service = service;

        public Task Consume(ConsumeContext<OrderMessage> context) =>
            _service.ProcessOrderAsync(context.Message);
    }
}
