using FluentValidation.AspNetCore;
using LawEnforcement.Application;
using LawEnforcement.Application.Behaviours;
using LawEnforcement.Infrastructure;
using LawEnforcement.Infrastructure.Persistence;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// SERWISY
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddControllers().AddFluentValidation()
    .AddNewtonsoftJson(s =>
    {
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        s.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    });


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
