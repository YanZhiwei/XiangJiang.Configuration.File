using System;
using System.Xml.Serialization;

namespace XiangJiang.Configuration.FileTests.Model
{
    [Serializable]
    public class RedisItemConfig
    {
        [XmlAttribute("txt")]
        public string Text { get; set; }
    }
}