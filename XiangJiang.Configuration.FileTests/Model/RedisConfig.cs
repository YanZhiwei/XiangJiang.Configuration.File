﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XiangJiang.Configuration.FileTests.Model
{
    [Serializable]
    public class RedisConfig
    {
        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        [XmlAttribute("WriteServerList")]
        public string WriteServerList
        {
            get;
            set;
        }

        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        [XmlAttribute("ReadServerList")]
        public string ReadServerList
        {
            get;
            set;
        }

        /// <summary>
        /// 最大写链接数
        /// </summary>
        [XmlAttribute("MaxWritePoolSize")]
        public int MaxWritePoolSize
        {
            get;
            set;
        }

        /// <summary>
        /// 最大读链接数
        /// </summary>
        [XmlAttribute("MaxReadPoolSize")]
        public int MaxReadPoolSize
        {
            get;
            set;
        }

        /// <summary>
        /// 自动重启
        /// </summary>
        [XmlAttribute("AutoStart")]
        public bool AutoStart
        {
            get;
            set;
        }

        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        [XmlAttribute("LocalCacheTime")]
        public int LocalCacheTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项
        /// </summary>
        [XmlAttribute("RecordeLog")]
        public bool RecordeLog
        {
            get;
            set;
        }

        public List<RedisItemConfig> RedisItems { get; set; }
    }
}