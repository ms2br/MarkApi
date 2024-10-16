using MarkAPI.Bussines.Dtos.RedisDtos;
using MarkAPI.Bussines.Dtos.TokenDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.ExternalServices.Implements
{
    public class BlackListService : IBlackListService
    {
        IRedisContext _redisContext { get; }
        JWT _jwt { get; }

        public BlackListService(IRedisContext redisContext,
            IOptionsMonitor<JWT> jwt)
        {
            _redisContext = redisContext;
            _jwt = jwt.CurrentValue;
        }

        public async Task SetAsync(string token)
        {
            if (!await TokenCheckAsync(token))
            {
                TimeSpan timeSpan =
                    new TimeSpan(int.Parse(_jwt.LifeSpan), 0, 0);
               await _redisContext.Database.StringSetAsync(token, token, timeSpan, When.NotExists);
            }
        }

        public async Task<bool> TokenCheckAsync(string token)
        => await _redisContext.Database.KeyExistsAsync(token);
    }
}
