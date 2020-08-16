using System;
using System.IO;
using System.Text;
using XiangJiang.Configuration.Abstractions;
using XiangJiang.Core;

namespace XiangJiang.Configuration.File
{
    /// <summary>
    ///     本地文件配置
    /// </summary>
    public sealed class FileConfigService : IConfigProvider
    {
        private readonly string _configFolder;
        private readonly string _fileType;
        public FileConfigService(string baseDirectory, string fileType)
        {
            Checker.Begin().NotNullOrEmpty(baseDirectory, nameof(baseDirectory))
                .NotNullOrEmpty(fileType, nameof(fileType));
            _configFolder = baseDirectory;
            _fileType = fileType;
        }

        public FileConfigService() : this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"), "json")
        {

        }

        /// <summary>
        ///     根据名称获取配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="pathExtrasCondition">路径额外条件</param>
        /// <returns>
        ///     配置内容
        /// </returns>
        public string GetConfig(string name, Func<string> pathExtrasCondition = null)
        {
            Checker.Begin().NotNullOrEmpty(name, nameof(name));
            var configPath = GetFilePath(name, pathExtrasCondition);
            return !System.IO.File.Exists(configPath) ? null : System.IO.File.ReadAllText(configPath);
        }

        /// <summary>
        ///     获取配置索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index">索引名称</param>
        /// <param name="pathExtrasCondition">路径额外条件</param>
        /// <returns>
        ///     索引
        /// </returns>
        public string GetClusteredIndex<T>(string index = null, Func<string> pathExtrasCondition = null)
            where T : class, new()
        {
            var fileName = $"{ConfigProviderHelper.CreateClusteredIndex<T>(index)}.{_fileType}";
            return pathExtrasCondition == null
                ? Path.Combine(_configFolder, fileName)
                : Path.Combine(_configFolder, pathExtrasCondition(), fileName);
        }

        /// <summary>
        ///     保存配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="content">配置内容</param>
        /// <param name="pathExtrasCondition">路径额外条件</param>
        public void SaveConfig(string name, string content, Func<string> pathExtrasCondition = null)
        {
            Checker.Begin().NotNullOrEmpty(name, nameof(name))
                .NotNullOrEmpty(content, nameof(content));
            var configPath = GetFilePath(name, pathExtrasCondition);
            System.IO.File.WriteAllText(configPath, content, Encoding.UTF8);
        }

        private string GetFilePath(string name, Func<string> pathExtrasCondition = null)
        {
            if (!Directory.Exists(_configFolder))
                Directory.CreateDirectory(_configFolder);
            var fileName = $"{name}.{_fileType}";
            return pathExtrasCondition == null
                ? Path.Combine(_configFolder, fileName)
                : Path.Combine(_configFolder, pathExtrasCondition(), fileName);
        }
    }
}