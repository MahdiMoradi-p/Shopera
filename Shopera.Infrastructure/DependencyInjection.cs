using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopera.Domain.Entities;
using Shopera.Infrastructure.Persistence;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Infrastructure.Repositories;
using Shopera.Application.IService.Invoice;
using Shopera.Application.Service.Invoice;
using Shopera.Application.IService.User;
using Shopera.Infrastructure.Services;
using Shopera.Application.IService.Buy;
using Shopera.Application.Service.Buy;
namespace Shopera.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddScoped<
                IInvoiceDetailRepository,
                InvoiceDetailRepository>();

            services.AddScoped<
                IInvoiceService,
                InvoiceService>();

            services.AddScoped<
                IInvoiceDetailService,
                InvoiceDetailService>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IBuyService, BuyService>();

            return services;
        }
    }
}