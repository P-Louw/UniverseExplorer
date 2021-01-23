using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.Units;
using Services.UniverseService;

namespace Tests.Services.Models.Utilities
{
    [TestFixture]
    public class JtokenValidData
    {
        [Test]
        public void ShouldReturnArraySunPlanet()
        {
            string[] arrayNames = new[] {"Planet", "Star"};
            var sut = ImportBuilder.SeedDataJTokens();

            CollectionAssert.IsNotEmpty(sut);
            Assert.That(sut, Is.InstanceOf<Dictionary<string, JToken>>());
            
            foreach (var name in arrayNames)
                CollectionAssert.IsNotEmpty(sut[name]);

            /*
            foreach (var model in arrayNames)
                {
                    CollectionAssert.IsNotEmpty(sut[model]);
                    CollectionAssert.Contains(sut[model], typeof(JToken));
                }
                */

         //   CollectionAssert.Contains(sut["Planet"], typeof(JObject));
        //    CollectionAssert.Contains(sut["Star"], typeof(JObject));

            /*CollectionAssert.IsNotEmpty(sut["Planet"]);
            CollectionAssert.IsNotEmpty(sut["Star"]);*/
        }

        [Test]
        public void TokensShouldHaveValues()
        {
            var sut = ImportBuilder.SeedDataJTokens();
            var name = sut["Planet"];
            var star = sut["Star"];
            foreach (var entry in name)
                {
                    var planet = new Planet();

                    planet.Name = (string) entry.SelectToken("name");
                    planet.OrbitDistance = (double) entry.SelectToken("orbitDistance");
                    planet.OrbitPeriod = (double) entry.SelectToken("orbitPeriod");
                    planet.KnownMoons = (int) entry.SelectToken("knownMoons");
                    planet.ID = (int) entry.SelectToken("id");
                    planet.PlanetarySystemID = (int) entry.SelectToken("planetarySystemID");
                    planet.Classification = (string) entry.SelectToken("classification");
                    planet.Diameter = (long) entry.SelectToken("diameter");
                    var min = (double) entry.SelectToken("surfaceTemperature.min");
                    var max = (double) entry.SelectToken("surfaceTemperature.max");
                    planet.OrbitPeriod = (double) entry.SelectToken("orbitPeriod");
                    var e = entry.SelectToken("id");
                }
        }
    }
}