namespace PinaryDevelopment.Framework.Pipelines.Tests.Unit.TestObjects
{
    using Newtonsoft.Json;

    public class TestObjectJsonParser : IParser<string, TestObject>
    {
        public TestObject Parse(string jsonInput)
        {
            return JsonConvert.DeserializeObject<TestObject>(jsonInput);
        }
    }
}
