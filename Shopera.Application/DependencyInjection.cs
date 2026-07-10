using Microsoft.Extensions.DependencyInjection;
using Shopera.Application.IService.User;
using Shopera.Application.Service.User;

namespace Shopera.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegisterService, RegisterService>();

            return services;
        }
    }
}