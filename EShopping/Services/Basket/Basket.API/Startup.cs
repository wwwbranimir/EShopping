using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
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
using Microsoft.AspNetCore.HttpOverrides;
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
            //net 8 specific changes
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    //TODO read the same from settings for prod deployment
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
           //     .AddVersionedApiExplorer(
           //options =>
           //{
           //    options.GroupNameFormat = "'v'VVV";
           //    options.SubstituteApiVersionInUrl = true;
           //});
            services.AddStackExchangeRedisCache(options =>
             {
                 options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
             });
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateShoppingCartCommandHandler));
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
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
            services.AddControllers(config =>
            {
                config.Filters.Add(new AuthorizeFilter(userPolicy));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://id-local.eshopping.com:44344";
                    options.Audience = "Basket";
                });

            //removed in mass transit version 8
            //services.AddMassTransitHostedService();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            var nginxPath = "/basket";
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"{nginxPath}/swagger/{description.GroupName}/swagger.json",
                            $"Basket API {description.GroupName.ToUpperInvariant()}");
                        options.RoutePrefix = string.Empty;
                    }

                    options.DocumentTitle = "Basket API Documentation";

                });
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
            app.UseCors("CorsPolicy");
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
