namespace PinaryDevelopment.Framework.Pipelines.Tests.Unit.TestObjects
{
    using System;

    public class SimpleString1PC : IPipelineComponent<TestObject>
    {
        public Func<IPipelineComponent<TestObject>> NextComponent { get; set; }

        public PipelineDelegate<TestObject> InvokeAsync
        {
            get => async testObject =>
            {
                testObject.String1 = "Hello";

                await NextComponent().InvokeAsync(testObject).ConfigureAwait(false);
            };
        }
    }
}
