using MailingService.Models;
using MailingService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Mailing Service
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection("EmailSettings"));

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
