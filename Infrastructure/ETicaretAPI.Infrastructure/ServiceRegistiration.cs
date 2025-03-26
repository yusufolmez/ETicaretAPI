using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Servıces;
using ETicaretAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService, FileService>();
        }
    }
}
