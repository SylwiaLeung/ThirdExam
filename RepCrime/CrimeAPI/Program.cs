using CrimeService.Behaviours;
using CrimeService.Data;
using CrimeService.Models.Dtos;
using CrimeService.Services.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// SERVICES

builder.Services.AddControllers().AddFluentValidation()
    .AddNewtonsoftJson(s =>
    {
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        s.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    });

builder.Services.AddScoped<ICrimeContext, CrimeContext>();
builder.Services.AddScoped<ICrimeRepository, CrimeRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IValidator<CrimeCreateDto>, CrimeValidationBehaviour>();
builder.Services.AddScoped<ErrorHandlingBehaviour>();

//RABBITMQ

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});
builder.Services.AddMassTransitHostedService();

// SZWAGIER

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// PIPELINE

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingBehaviour>();

app.UseAuthorization();

app.MapControllers();

app.Run();
