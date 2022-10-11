namespace FinalEngine.Editor.Services.Extensions
{
    using System;
    using System.Reflection;
    using FinalEngine.Editor.Services.Instructions;
    using FluentValidation;
    using MediatR;
    using Micky5991.EventAggregator.Interfaces;
    using Micky5991.EventAggregator.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddSingleton<IEventAggregator, EventAggregatorService>();
            services.AddSingleton<IInstructionsManager, InstructionsManager>();

            return services;
        }
    }
}
