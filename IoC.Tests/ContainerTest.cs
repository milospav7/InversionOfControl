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
        [Test]
        public void CreateInstanceWithNoParams()
        {
            var subject = (A)Container.GetInstance(typeof(A));
            subject.Should().BeOfType<A>();
        }

        [Test]
        public void CreateInstanceWithParams()
        {
            var subject = (B)Container.GetInstance(typeof(B));
            subject.A.Should().BeOfType<A>();
        }

        [Test]
        public void ParameterlessConstructorAllowe()
        {
            var subject = (C)Container.GetInstance(typeof(C));
            subject.Invoked.Should().BeTrue();
        }

        [Test]
        public void GenericInstanceAllowed()
        {
            var subject = Container.GetInstance<A>();
            subject.Should().BeOfType<A>();
        }


        class A
        {

        }

        class B
        {
            public A A { get; }

            public B()
            {

            }

            public B(A a)
            {
                A = a;
            }
        }

        class C
        {
            public bool? Invoked { get; set; }

            public C()
            {
                Invoked = true;
            }
        }

    }


    [TestFixture]
    public class Container_Register : ContainerTestBase
    {
        [Test]
        public void RegisterFromInterface()
        {
            Container.Register<ICar, AudiA5>(); // DI part
            var subject = Container.GetInstance<ICar>(); // then require interface
            subject.Should().BeOfType<AudiA5>(); // check if DI done successfuly
        }

        interface ICar
        {
            string EngineType { get; }
        }

        class AudiA6 : ICar
        {
            public string EngineType => "Diesel";
        }

        class AudiA5 : ICar
        {
            public string EngineType => "Petrol";
        }

        class TeslaP90d : ICar
        {
            public string EngineType => "Electric";
        }
    }

}