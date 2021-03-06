using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.CelestialObjects;
using Services.Core.DataModels.Units;
using Services.Core.Models.DTO;
using Services.UniverseService;
using Services.UniverseService.Context;
using Tests.Services.UniverseEf.DBContextDev;
using Tests.Services.UniverseEf.Seed;

namespace Tests.Services.UniverseEf
{
    public class AssignmentsValidData
    {
        private IUniverseService sut;
        private DbContextOptions<UniverseContext> options;
        private Func<UniverseContext> ctx;

        [SetUp]
        public void Setup()
        {
            options = ContextBuilder.InitDbInMem();
            ctx = () => new UniverseContext(options);
        }

        [TearDown]
        public void CleanUp()
        {
            using (var context = new UniverseContext(options))
                context.Database.EnsureDeleted();
        }

        [Test]
        [Category("Assignment: 2")]
        public void PlanetsOrderedAlphabeticalShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.SimpleDataA());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.PlanetsOrderedAlphabetical();

                    // Assert:
                    CollectionAssert.AllItemsAreNotNull(result);
                    CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Planet));
                    CollectionAssert.AllItemsAreUnique(result);

                    Assert.That(result.Select(n => n.Name),
                        Is.Ordered.Ascending);
                    //   Is.EqualTo(ValidSimpleData.ExpectedAlphabetical));

                    Assert.That(result, Has.Count.EqualTo(14));
                }
        }

        [Test]
        [Category("Assignment: 3")]
        public void PlanetsTempAboveZeroShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.SimpleDataA());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.PlanetsTempAboveZero();

                    // Assert:
                    CollectionAssert.Contains(result.Select(v => v.Classification), "Planet");
                    CollectionAssert.Contains(result.Select(v => v.Classification), "Dwarf planet");

                    var temperatures = result.Select(r => r.SurfaceTemperature);
                    foreach (Temperature temp in temperatures)
                        Assert.That(new[] {temp.Max, temp.Min}, Has.Some.Positive);
                }
        }

        [Test]
        [Category("Assignment: 4")]
        public void NameLetterConstraintShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.NameConstraintData());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.PlanetNameLetterConstraint().ToList();

                    // Assert:
                    foreach (var cat in result.Select(p => p.Classification))
                        StringAssert.AreEqualIgnoringCase("Planet", cat);

                    Assert.That(result.Count(), Is.EqualTo(4));
                    foreach (var name in result.Select(p => p.Name))
                        StringAssert.IsMatch(@"[ptPT]", name);
                }
        }

        [Test]
        [Category("Assignment: 5")]
        public void NameLengthDescendingShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.NameConstraintData());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.PlanetsNameLengthDescending();

                    // Assert:
                    Assert.That(result.Select(v =>
                        v.Name.Length), Is.Ordered.Descending);
                }
        }

        [Test]
        [Category("Assignment: 6")]
        public void DistanceToSunShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.SimpleDataA());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.PlanetDistanceToSunAscending();

                    // Assert:
                    Assert.That(result.Select(v => 
                        v.OrbitDistance), Is.Ordered.Ascending);
                }
        }


        [Test]
        [Category("Assignment: 7")]
        public void ByMoonAmountShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.SimpleDataA());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.DwarfPlanetByMoonAmount();

                    // Assert:
                    var categories = result.Select(v => v.Classification);
                    foreach (var category in categories)
                        Assert.That(category, Is.EqualTo("Dwarf planet"));

                    Assert.That(result.Select(m => m.KnownMoons), Is.Ordered);
                }
        }


        [Test]
        [Category("Assignment: 8")]
        public void TotalMoonsShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.SimpleDataA());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.TotalMoons();

                    // Assert:
                    Assert.That(result, Is.Positive);
                    Assert.That(result, Is.EqualTo(67));
                }
        }

        [Test]
        [Category("Assignment: 9")]
        public void DwarfSortedByDiameterShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.DwarfPlanetSimpleData());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.DwarfPlanetsSortedDiameter();

                    // Assert:
                    Assert.That(result.Select(p => p.Diameter), Is.Ordered.Ascending);
                }
        }

        [Test]
        [Category("Assignment: 10")]
        public void DwarfAverageMoonsShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.DwarfPlanetSimpleData());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.AverageMoonsPerDwarfPlanet();

                    // Assert:
                    Assert.That(result, Is.EqualTo(5).Within(0.5));
                }
        }


        [Test]
        [Category("Assignment: 11")]
        public void AverageSurfaceTempShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.Planets.AddRange(ValidSimpleData.SimpleDataA());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.AverageSurfaceTemps();

                    // Assert:
                    Assert.That(result, Contains.Key(
                        "Dwarf planet").And.ContainKey("Planet"));
                    
                    CollectionAssert.AreEquivalent(ValidSimpleData.ExpectedAverageTempDwarf(),
                        result["Dwarf planet"].Select(x =>
                            x.AverageTemperature).ToList());
                }
        }


        [Test]
        [Category("Assignment: 12")]
        public void TotalBodyAmountShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.PlanetarySystems.Add(ValidSimpleData.TestStarSystem());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.TotalBodyAmount();

                    // Assert:
                    Assert.That(result, Is.EqualTo(14));
                }
        }

        [Test]
        [Category("Assignment: 13")]
        public void ClosestPlanetsShouldReturn()
        {
            using (var context = new UniverseContext(options))
                {
                    // Setup:
                    context.PlanetarySystems.Add(ValidSimpleData.TestStarSystem());
                    context.SaveChanges();
                    sut = new UniverseService(context);

                    // Act:
                    var result = sut.ClosestNeighbourPlanets();

                    // Assert:
                    Assert.That(result.MeasuredDistance, Is.EqualTo(2));
                }
        }
    }
}