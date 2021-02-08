using System.Collections.Generic;
using System.Collections.Specialized;
using Services.Core.DataModels.Units;
using Services.Core.Models.DTO;
using Services.UniverseService;
using static System.Console;

namespace App.UniverseExplorer
{
    
    public class MainWindow 
    {
        private bool active = true;
        private readonly IUniverseService _db;

        public MainWindow(IUniverseService db) => 
            _db = db;
        
        public void OnWindowLoad()
        {
            PrintFunc.EnumerableResult(
                () => _db.PlanetsOrderedAlphabetical(),
                "Planets ordered alphabetically:",
                (x) => $"Name: {x.Name}");
                    
                        
            PrintFunc.EnumerableResult(
                () => _db.PlanetsTempAboveZero(),
                "Planets with a temperature above 0:",
                (x) => $"{x.Name} -> {x.SurfaceTemperature.Max} °C"
                );
            
            PrintFunc.EnumerableResult(
                () => _db.PlanetNameLetterConstraint(),
                "Planets with letter p & t case insensitive:",
                (x) => $"{x.Name}");            
            
            PrintFunc.EnumerableResult(
                () => _db.PlanetsNameLengthDescending(),
                "Planets sorted by name length:",
                (x) => $"Length: {x.Name.Length} Name: {x.Name}");            
            
            PrintFunc.EnumerableResult(
                () => _db.PlanetDistanceToSunAscending(),
                "Planets sorted by distance to the sun:",
                (x) => $"Name: {x.Name} => Distance: {x.OrbitDistance}"); 
            
            PrintFunc.EnumerableResult(
                () => _db.DwarfPlanetByMoonAmount(),
                "Moon amount dwarf planets:",
                (x) => $"Name: {x.Name} -> {x.KnownMoons}");
            
            PrintFunc.DigitResult(
                () => _db.TotalMoons(),
                "Total amount of known moons:",
                (x) => $"Amount: {x}");
            
            PrintFunc.EnumerableResult(
                () => _db.DwarfPlanetsSortedDiameter(),
                "Dwarf planets sorted by diameter:",
                (x) => $"Name: {x.Name} -> Diameter: {x.Diameter}"); 
            
            PrintFunc.DigitResult(
                () => _db.AverageMoonsPerDwarfPlanet(),
                "Average moons per dwarf planet:",
                (x) => $"Amount: {x}"); 
            
            var task_11 = _db.AverageSurfaceTemps();
            WriteLine("Average surface temperatures of all types:");
            foreach (var entry in task_11)
                {
                    WriteLine("Classification: {0}", entry.Key);
                    foreach (var cat in entry.Value)
                        {
                            WriteLine("Type: {0}\nAvg: {1}\nMax: {2}\nMin: {3}\n", 
                                cat.Classification, cat.AverageTemperature, cat.Max, cat.Min);
                        }
                }
            WriteLine("Press enter to continue");
            ReadLine();
            
            PrintFunc.DigitResult(
                () => _db.TotalBodyAmount(),
                "Total planets and other bodies:",
                (x) => $"Amount: {x}");  
            
            PrintFunc.DtoResult<TwoPlanetDifference>(
                () => _db.ClosestNeighbourPlanets(),
                "Closest neighbouring planets:",
                (x) => $"{x.PlanetA.Name} & {x.PlanetB.Name} are only {x.MeasuredDistance} Km apart.");  
            
            active = false;
        }
    }
}