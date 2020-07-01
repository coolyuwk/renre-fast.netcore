using Microsoft.Extensions.DependencyInjection;


namespace RenRen.Domain.Redis
{
    public static class RenRenRedisServiceCollectionExtensions
    {
        public static IServiceCollection AddDistributedRedisCache(this IServiceCollection services, RedisCacheOptions setupAction)
        {
            services.AddSingleton(new RedisClient(setupAction));
            return services;
        }
    }
}
