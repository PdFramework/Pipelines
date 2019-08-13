namespace PinaryDevelopment.Framework.Pipelines.Tests.Unit.TestObjects
{
    using System;

    public class BeforeSimpleString1AfterSimpleString2PC : IPipelineComponent<TestObject>
    {
        public Func<IPipelineComponent<TestObject>> NextComponent { get; set; }

        public PipelineDelegate<TestObject> InvokeAsync
        {
            get => async testObject =>
            {
                testObject.String1 = "World";

                await NextComponent().InvokeAsync(testObject).ConfigureAwait(false);

                testObject.String2 = "Hello";
            };
        }
    }
}
