using EventBus.Messaging;
using FluentValidation.AspNetCore;
using LawEnforcement.Application;
using LawEnforcement.Application.Behaviours;
using LawEnforcement.Application.HttpClients;
using LawEnforcement.Infrastructure;
using LawEnforcement.Infrastructure.Persistence;
using LawEnforcementAPI.EventBusConsumer;
using LawEnforcementAPI.HttpClients;
using MassTransit;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// SERWISY

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddHttpClient<ICrimeEventClient, CrimeEventClient>();

builder.Services.AddControllers().AddFluentValidation()
    .AddNewtonsoftJson(s =>
    {
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        s.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    });

//RABBITMQ

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<CrimeEventConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.CrimeQueue, c =>
        {
            c.ConfigureConsumer<CrimeEventConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

// SZWAGIER

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// PIPELINE

EnforcementContextSeed.Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<UnhandledExceptionsBehaviour>();

app.UseAuthorization();

app.MapControllers();

app.Run();
