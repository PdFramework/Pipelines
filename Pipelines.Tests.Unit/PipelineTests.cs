namespace Pipelines.Tests.Unit
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PinaryDevelopment.Framework.Pipelines;
    using PinaryDevelopment.Framework.Pipelines.Tests.Unit.TestObjects;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class PipelineTests
    {
        private static IServiceProvider ServiceProvider { get; set; }
        private TestObject NewTestObject => new TestObject();

        [ClassInitialize]
        public static void PipelineTestsInitialize(TestContext testContext)
        {
            ServiceProvider = new ServiceCollection()
                        .AddSingleton<SimpleString1PC>()
                        .AddSingleton<SimpleString2PC>()
                        .AddSingleton<BeforeSimpleString1AfterSimpleString2PC>()
                        .AddSingleton<BeforeSimpleString2AfterSimpleString1PC>()
                        .BuildServiceProvider();
        }

        [TestMethod]
        public async Task Pipeline_Component_Should_Update_String1()
        {
            var updatedTestObject = await new Pipeline<TestObject>(ServiceProvider)
                                            .Use<SimpleString1PC>()
                                            .Process(NewTestObject)
                                            .ConfigureAwait(false);

            Assert.AreEqual(updatedTestObject.String1, "Hello");
            Assert.IsNull(updatedTestObject.String2);
        }

        [TestMethod]
        public async Task Pipeline_Component_Should_Update_String2()
        {
            var updatedTestObject = await new Pipeline<TestObject>(ServiceProvider)
                                            .Use<SimpleString2PC>()
                                            .Process(NewTestObject)
                                            .ConfigureAwait(false);

            Assert.IsNull(updatedTestObject.String1);
            Assert.AreEqual(updatedTestObject.String2, "World");
        }

        [TestMethod]
        public async Task Pipeline_Components_Should_Update_String1_And_String2()
        {
            var updatedTestObject = await new Pipeline<TestObject>(ServiceProvider)
                                            .Use<SimpleString1PC>()
                                            .Use<SimpleString2PC>()
                                            .Process(NewTestObject)
                                            .ConfigureAwait(false);

            Assert.AreEqual(updatedTestObject.String1, "Hello");
            Assert.AreEqual(updatedTestObject.String2, "World");
        }

        [TestMethod]
        public async Task Pipeline_Components_Should_Contain_String1_And_String2_From_First_Component()
        {
            var updatedTestObject = await new Pipeline<TestObject>(ServiceProvider)
                                            .Use<BeforeSimpleString2AfterSimpleString1PC>()
                                            .Use<SimpleString1PC>()
                                            .Process(NewTestObject)
                                            .ConfigureAwait(false);

            Assert.AreEqual(updatedTestObject.String1, "There");
            Assert.AreEqual(updatedTestObject.String2, "World");
        }

        [TestMethod]
        public async Task Pipeline_Components_Should_Contain_String1_From_First_Component_And_String2_From_Second_Component()
        {
            var updatedTestObject = await new Pipeline<TestObject>(ServiceProvider)
                                            .Use<BeforeSimpleString1AfterSimpleString2PC>()
                                            .Use<BeforeSimpleString2AfterSimpleString1PC>()
                                            .Process(NewTestObject)
                                            .ConfigureAwait(false);

            Assert.AreEqual(updatedTestObject.String1, "There");
            Assert.AreEqual(updatedTestObject.String2, "Hello");
        }
    }
}
