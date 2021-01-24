using NUnit.Framework;
using Services.UniverseService;
using Tests.Services.UniverseEf.DBContextDev;

namespace Tests.Services.UniverseEf
{
    public class Tests
    {
        private IUniverseService sut;
        [SetUp]
        public void Setup()
        {
            sut = new UniverseService(
                new InMemoryEf());
        }
        
        

        [Test]
        public void Test1()
        {
            Assert.That(sut.TotalMoons(), Is.Positive);
        }
    }
}