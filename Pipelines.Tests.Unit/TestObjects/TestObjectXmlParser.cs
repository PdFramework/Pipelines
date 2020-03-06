namespace PinaryDevelopment.Framework.Pipelines.Tests.Unit.TestObjects
{
    using System.IO;
    using System.Xml.Serialization;

    public class TestObjectXmlParser : IParser<string, TestObject>
    {
        public TestObject Parse(string xmlInput)
        {
            using (var stringReader = new StringReader(xmlInput))
            {
                return (TestObject)new XmlSerializer(typeof(TestObject)).Deserialize(stringReader);
            }
        }
    }
}
