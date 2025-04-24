
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderMS.Infra.Repositories;
using OrderMS.Services;
using Project_BTG_Pactual_Api.Applicacao.Consumers;
using Project_BTG_Pactual_Api.Applicacao.interfacesServices;
using Project_BTG_Pactual_Api.Dominion.InterfacesRepositores;
using Project_BTG_Pactual_Api.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddDbContext<OrderDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        var rmq = builder.Configuration.GetSection("RabbitMQ");
        var host = rmq["Host"] ?? "rabbitmq";
        var port = int.TryParse(rmq["Port"], out var p) ? p : 5672;

        cfg.Host(host, (ushort)port, "/", h =>
        {
            h.Username(rmq["Username"]);
            h.Password(rmq["Password"]);
        });

        cfg.ReceiveEndpoint(rmq["QueueName"], e =>
        {
            e.ConfigureConsumer<OrderConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    db.Database.Migrate(); 
}
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
