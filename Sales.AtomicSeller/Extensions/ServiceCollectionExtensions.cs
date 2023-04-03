using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sales.AtomicSeller.Providers;
using Sales.AtomicSeller.Repositories;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Business.Services;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Sales.AtomicSeller.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Inject business services.
        /// </summary>
        /// <param name="services"></param>
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IConverter),new SynchronizedConverter(new PdfTools()));

            services.AddScoped<IIdentityProvider, IdentityProvider>();
            // Service
            services.AddScoped<IRepository<AtomicService>, Repository<AtomicService>>();
            services.AddScoped<IAtomicServiceService, AtomicServiceService>();


            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IRepository<OrderDetails>, Repository<OrderDetails>>();
            services.AddScoped<IOrderDetailsService, OrderDetailsService>();

            services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            services.AddScoped<ICartItemService, CartItemService>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
        }
    }
}

