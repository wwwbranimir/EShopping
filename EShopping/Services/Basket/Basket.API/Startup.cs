using Basket.Application.GrpcService;
using Basket.Application.Handlers;
using Basket.Core.Repositories;
using Discount.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace Basket.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            services.AddStackExchangeRedisCache(options =>
             {
                 options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
             });
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateShoppingCartCommandHandler));
            });
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<DiscountGrpcService>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
          (o => o.Address = new Uri(Configuration["GrpcSettings:DiscountUrl"]));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
            });
            //add health check for redis
            services.AddHealthChecks()
                .AddRedis(Configuration["CacheSettings:ConnectionString"], name: "Redis Cache Health Check", HealthStatus.Degraded);
            services.AddMassTransit(cfg =>
            {
               cfg.UsingRabbitMq((ctx, cfg) =>
               {
                   cfg.Host(Configuration["EventBusSettings:HostAddress"]);
               });

            });
           

            var userPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //Identity server changes
            services.AddControllers(config => {
                config.Filters.Add(new AuthorizeFilter(userPolicy));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:9009";
                    options.Audience = "Basket";
                });

            //removed in mass transit version 8
            //services.AddMassTransitHostedService();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
            }
          
            app.UseRouting();
            app.Use(async (context, next) =>
            {
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (authHeader != null)
                {
                    Console.WriteLine($"Authorization Header: {authHeader}");
                }
                else
                {
                    Console.WriteLine("Authorization Header missing");
                }

                await next.Invoke();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
