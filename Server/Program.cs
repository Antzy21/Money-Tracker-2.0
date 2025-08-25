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

builder.Services.AddSingleton<CsvService>();

const string vueClientCorsPolicy = "vue_client";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: vueClientCorsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(vueClientCorsPolicy);

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();