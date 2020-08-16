using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XiangJiang.Configuration.Abstractions;
using XiangJiang.Configuration.File;
using XiangJiang.Configuration.FileTests.Model;
using XiangJiang.Infrastructure.Serializer.Json;

namespace XiangJiang.Configuration.FileTests
{
    [TestClass()]
    public class FileConfigServiceTests
    {
        private ConfigContext _configContext;

        [TestInitialize]
        public void Init()
        {
            _configContext = new ConfigContext(new FileConfigService(), new JsonSerializer());
        }

        [TestMethod()]
        public void FileConfigServiceTest()
        {
            RedisConfig redisConfig = new RedisConfig
            {
                AutoStart = true,
                LocalCacheTime = 10,
                MaxReadPoolSize = 1024,
                MaxWritePoolSize = 1024,
                ReadServerList = "10",
                RecordeLog = true,
                WriteServerList = "10",
                RedisItems = new List<RedisItemConfig>
                {
                    new RedisItemConfig() {Text = "MasterChief"}, new RedisItemConfig() {Text = "Config."}
                }
            };

            _configContext.Save(redisConfig, "prod");
            _configContext.Save(redisConfig, "alpha");

            RedisConfig prodRedisConfig = _configContext.Get<RedisConfig>("prod");
            Assert.IsNotNull(prodRedisConfig);

            prodRedisConfig = _configContext.Get<RedisConfig>("prod");//文件缓存测试
            Assert.IsNotNull(prodRedisConfig);

            RedisConfig alphaRedisConfig = _configContext.Get<RedisConfig>("alpha");
            Assert.IsNotNull(alphaRedisConfig);

            DaoConfig daoConfig = new DaoConfig
            {
                Log = "server=localhost;database=Sample;uid=sa;pwd=sasa"
            };
            _configContext.Save(daoConfig, "prod");
            _configContext.Save(daoConfig, "alpha");
            DaoConfig prodDaoConfig = _configContext.Get<DaoConfig>("prod");
            Assert.IsNotNull(prodDaoConfig);

            DaoConfig alphaDaoConfig = _configContext.Get<DaoConfig>("alpha");
            Assert.IsNotNull(alphaDaoConfig);
        }


    }
}