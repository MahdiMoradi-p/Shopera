using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shopera.Application.IService.Order;
using Shopera.Application.IService.Product;
using Shopera.Application.IService.User;
using Shopera.Application.Service.Order;
using Shopera.Application.Service.Product;
using Shopera.Application.Service.User;
using Shopera.Application.Validators.User;


namespace Shopera.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductDetailService, ProductDetailService>();
            services.AddScoped<IOrderService , OrderService>();
            services.AddScoped<IOrderDetailService , OrderDetailService>();
            return services;
        }
    }
}