using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Kledex.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static IServiceCollection AddSerializerSettings(this IServiceCollection services, JsonSerializerSettings settings)
        {
            services.AddSingleton(settings);

            return services;
        }
    }
}