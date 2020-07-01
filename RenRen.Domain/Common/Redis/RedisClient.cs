using StackExchange.Redis;
using System;
using System.Collections.Concurrent;

namespace RenRen.Domain.Redis
{
    public class RedisClient : IDisposable
    {
        private RedisCacheOptions _options;
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;

        public RedisClient(RedisCacheOptions options)
        {
            _options = options;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }

        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect()
        {
            return _connections.GetOrAdd(_options.InstanceName, p => ConnectionMultiplexer.Connect(_options.ConnectionString));
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="db">默认为0：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return GetConnect().GetDatabase(_options.DefaultDB);
        }

        public IServer GetServer(string configName = null, int endPointsIndex = 0)
        {
            var confOption = ConfigurationOptions.Parse(_options.ConnectionString);
            return GetConnect().GetServer(confOption.EndPoints[endPointsIndex]);
        }

        public ISubscriber GetSubscriber(string configName = null)
        {
            return GetConnect().GetSubscriber();
        }

        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
    }

    public class RedisCacheOptions
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
        public int DefaultDB { get; set; } = 0;
    }
}
