
namespace PinaryDevelopment.Framework.Pipelines.Tests.Unit.TestObjects
{
    using Newtonsoft.Json;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("test-object")]
    public class TestObject
    {
        [XmlElement("string1")]
        [JsonProperty("string1")]
        public string String1 { get; set; }

        [XmlElement("string2")]
        [JsonProperty("string2")]
        public string String2 { get; set; }
    }
}
