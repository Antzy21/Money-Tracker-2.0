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

const string localhostCorsPolicy = "localhost_client";
const string dockerCorsPolicy = "docker_client";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: localhostCorsPolicy,
        policy  =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    options.AddPolicy(name: dockerCorsPolicy,
        policy  =>
        {
            policy.WithOrigins("http://client:5174")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(localhostCorsPolicy);
app.UseCors(dockerCorsPolicy);

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();