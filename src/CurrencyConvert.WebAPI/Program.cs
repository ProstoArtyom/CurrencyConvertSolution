using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Application.Services;
using CurrencyConvert.Application.Validators;
using CurrencyConvert.Infrastructure.Banks;
using CurrencyConvert.Infrastructure.Configuration;
using CurrencyConvert.Infrastructure.Providers;
using CurrencyConvert.Infrastructure.Providers.AlfaBank;
using CurrencyConvert.Infrastructure.Providers.Nbrb;
using CurrencyConvert.WebAPI.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ConvertRequestValidator>();

builder.Services.Configure<BankSettings>(
    builder.Configuration.GetSection(BankSettings.SectionName));

var bankSettings = builder.Configuration
    .GetSection(BankSettings.SectionName)
    .Get<BankSettings>()!;

builder.Services.AddHttpClient<NbrbRateProvider>(c =>
    c.BaseAddress = new Uri(bankSettings.Nbrb.BaseUrl));
builder.Services.AddHttpClient<AlfaBankRateProvider>(c =>
    c.BaseAddress = new Uri(bankSettings.Alfabank.BaseUrl));

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IBankRegistry, BankRegistry>();

builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

builder.Services.AddTransient<IBankRateProvider, NbrbRateProvider>();
builder.Services.AddTransient<IBankRateProvider, AlfaBankRateProvider>();
builder.Services.AddTransient<IBankRateProviderFactory, BankRateProviderFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
