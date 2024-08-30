using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoneyTracker2.Data;
using MoneyTracker2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MoneyTrackerContext>();
builder.Services.AddSingleton<TransactionImportService>();

var app = builder.Build();

app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();