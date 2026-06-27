using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Application.Services;
using CurrencyConvert.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IBankRegistry, BankRegistry>();

builder.Services.AddScoped<IBankService, BankService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();
