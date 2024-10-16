using MarkAPI.Bussines.Dtos.RedisDtos;
using MarkAPI.Bussines.Exceptions.RedisExceptions;
using MarkAPI.Bussines.ExternalContext.Interfaces;
using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.ExternalContext.Implements
{
    public class RedisContext :
        IRedisContext
    {
        public IDatabase Database { get; set; }
        public Lazy<ConnectionMultiplexer> MasterConnection { get; set; }
        ConfigurationOptions sentinelConfiguration { get; } = new()
        {
            AbortOnConnectFail = false,
            CommandMap = CommandMap.Sentinel
        };


        public async Task GetDatabaseAsync(RedisOption redisOption, int dbNumber = 0)
        {
            sentinelConfiguration.EndPoints.Clear();
            foreach (string sentinel in redisOption.Sentinels)
                sentinelConfiguration.EndPoints.Add(sentinel);
            await RedisConnectionAsync(redisOption);
            Database = MasterConnection.Value.GetDatabase(dbNumber);
        }

        async Task RedisConnectionAsync(RedisOption redisOption)
        {
            using (ConnectionMultiplexer connectionMultiplexer = await ConnectionMultiplexer.SentinelConnectAsync(sentinelConfiguration))
            {
                EndPoint masterEndPoint = await GetEndPointAsync(connectionMultiplexer, redisOption.MasterName);
                string getLocalIp = GetLocalMasterIp(masterEndPoint);
                MasterConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.ConnectAsync(getLocalIp).Result);
            }

        }

        async Task<EndPoint> GetEndPointAsync(IConnectionMultiplexer sentinelConnection, string masterName)
        {
            foreach (EndPoint endPoint in sentinelConnection.GetEndPoints())
            {
                IServer server = sentinelConnection.GetServer(endPoint);
                if (server.IsConnected)
                    return await server.SentinelGetMasterAddressByNameAsync(masterName);
            }
            throw new RedisMasterNameException($"Failed to find master endpoint for Redis Sentinel with master name: {masterName}");
        }

        string GetLocalMasterIp(EndPoint masterEndPoint)
        {
            return masterEndPoint.ToString() switch
            {
                "172.19.0.8:6379" => "localhost:1453",
                "172.19.0.7:6379" => "localhost:1454",
                "172.19.0.6:6379" => "localhost:1455",
                "172.19.0.5:6379" => "localhost:1456",
                _ => throw new UnsupportedEndpointException($"Unsupported master endpoint: {masterEndPoint}")
            };
        }
    }
}
