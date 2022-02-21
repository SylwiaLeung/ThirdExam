using EventBus.Messaging;
using MailingService.EventBusConsumer;
using MailingService.Models;
using MailingService.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// SERVICES

builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection("EmailSettings"));

// RABBITMQ

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<CrimeUpdateConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.CrimeUpdateQueue, c =>
        {
            c.ConfigureConsumer<CrimeUpdateConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

// SWAGGER

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
