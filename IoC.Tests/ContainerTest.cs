using FluentAssertions;
using IoC;
using NUnit.Framework;

namespace Tests
{
    public class ContainerTestBase
    {

        protected Container Container;

        [SetUp]
        public void BeforeEach()
        {
            Container = new Container();
        }

        [TearDown]
        public void AfterEach()
        {
            Container = null;
        }
    }

    [TestFixture]
    public class Container_GetInstance : ContainerTestBase
    {
        public void CreateInstanceWithParams()
        {
            var subject = (A)Container.GetInstance(typeof(A));
            subject.Should().BeOfType<A>();
        }

        class A
        {

        }

    }

}