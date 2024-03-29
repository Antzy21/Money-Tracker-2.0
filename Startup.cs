using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyTracker.Data;
using MoneyTracker.Services;
using MoneyTracker.Data.DataAccessLayers;

namespace MoneyTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string clientUi = "client_ui";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<TransactionImportService>();

            services.AddDbContext<BankContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BankContext")));

            services.AddScoped<TransactionRepository>();
            services.AddScoped<CategoryRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: clientUi,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080",
                                            "http://192.168.0.16:8080/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(clientUi);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
