namespace PinaryDevelopment.Framework.Pipelines.Tests.Unit.TestObjects
{
    using System;

    public class BeforeSimpleString2AfterSimpleString1PC : IPipelineComponent<TestObject>
    {
        public Func<IPipelineComponent<TestObject>> NextComponent { get; set; }

        public PipelineDelegate<TestObject> InvokeAsync
        {
            get => async testObject =>
            {
                testObject.String2 = "World";

                await NextComponent().InvokeAsync(testObject).ConfigureAwait(false);

                testObject.String1 = "There";
            };
        }
    }
}
