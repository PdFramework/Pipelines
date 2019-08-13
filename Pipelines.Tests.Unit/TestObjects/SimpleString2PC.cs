namespace PinaryDevelopment.Framework.Pipelines.Tests.Unit.TestObjects
{
    using System;

    public class SimpleString2PC : IPipelineComponent<TestObject>
    {
        public Func<IPipelineComponent<TestObject>> NextComponent { get; set; }

        public PipelineDelegate<TestObject> InvokeAsync
        {
            get => async testObject =>
            {
                testObject.String2 = "World";

                await NextComponent().InvokeAsync(testObject).ConfigureAwait(false);
            };
        }
    }
}
