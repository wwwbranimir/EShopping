﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;
using System.Reflection;

namespace Ordering.Infrastructure.Extensions
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<OrderContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();

            return serviceCollection;
        }
    }
}
