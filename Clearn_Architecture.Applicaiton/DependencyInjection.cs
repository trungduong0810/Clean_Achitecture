using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean_Architecture.Applicaiton
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Đăng ký tất cả các FluentValidation validator từ Assembly hiện tại
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Đăng ký MediatR behaviors nếu cần
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Common.Behaviours.ValidationBehaviour<,>));

            return services;
        }
    }
}
