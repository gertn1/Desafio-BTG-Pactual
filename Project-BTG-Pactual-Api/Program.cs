

//using MassTransit;
//using Microsoft.EntityFrameworkCore;
//using OrderMS.Consumers;
//using OrderMS.Data;


//var builder = WebApplication.CreateBuilder(args);

//// Configurar DB
//builder.Services.AddDbContext<OrderDbContext>(opts =>
//    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Configurar MassTransit + RabbitMQ
//builder.Services.AddMassTransit(x => {
//    x.AddConsumer<OrderConsumer>();

//    x.UsingRabbitMq((ctx, cfg) => {
//        var rmq = builder.Configuration.GetSection("RabbitMQ");
//        cfg.Host(rmq["Host"], h => {
//            h.Username(rmq["Username"]);
//            h.Password(rmq["Password"]);
//        });
//        cfg.ReceiveEndpoint(rmq["QueueName"], e => {
//            e.ConfigureConsumer<OrderConsumer>(ctx);
//        });
//    });
//});
//builder.Services.AddMassTransitHostedService();

//// MVC / Controllers / Swagger
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();
//app.UseSwagger();
//app.UseSwaggerUI();
//app.MapControllers();
//app.Run();


using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderMS.Consumers;
using OrderMS.Data;

var builder = WebApplication.CreateBuilder(args);

// 1) Configurar DB
builder.Services.AddDbContext<OrderDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Configurar MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        var rmq = builder.Configuration.GetSection("RabbitMQ");
        // lê host e porta (porta opcional, default 5672)
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

// 3) MVC / Controllers / Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
