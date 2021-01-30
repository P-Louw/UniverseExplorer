using App.Core.GUI;
using Services.UniverseService;
using static System.Console;

namespace App.UniverseExplorer
{
    //https://github.com/migueldeicaza/gui.cs
    
    public class MainWindow : CLIWindow
    {
        private bool active = true;
        private readonly IUniverseService _db;

        public MainWindow(IUniverseService db) => 
            _db = db;
        
        public override void OnWindowLoad()
        {
            var task_2 = _db.PlanetsOrderedAlphabetical();
            WriteLine("Planets ordered alphabetically:");
            foreach (var entry in task_2)
                {
                    WriteLine("Name: {0}", entry.Name);
                }
            WriteLine("Press enter to continue");
            ReadLine();
            
            var task_3 = _db.PlanetsTempAboveZero();
            WriteLine("Planets with a temperature above 0:");
            foreach (var entry in task_3)
                {
                    WriteLine("{0} -> {1} °C", entry.Name, entry.SurfaceTemperature.Max);
                }
            WriteLine("Press enter to continue");
            ReadLine();
            
            var task_4 = _db.PlanetNameLetterConstraint();
            WriteLine("Planets with letter p & t case insensitive:");
            foreach (var entry in task_4)
                {
                    WriteLine("{0}",entry.Name);
                }
            WriteLine("Press enter to continue");
            ReadLine();
            
            var task_5 = _db.PlanetsNameLengthDescending();
            WriteLine("Planets sorted by name length:");
            foreach(var planet in task_5) WriteLine($"{planet.Name.Length} -> {planet.Name}");
            WriteLine("Press enter to continue");
            ReadLine();
            
            var task_6 = _db.PlanetDistanceToSunAscending();
            WriteLine("Planets sorted by distance to the sun ascending:");
            foreach (var entry in task_6)
                {
                    WriteLine("{0} -> {1}", entry.Name, entry.OrbitDistance);
                }
            WriteLine("Press enter to continue");
            ReadLine(); 
            
            var task_7 = _db.DwarfPlanetByMoonAmount();
            WriteLine("Moon amount dwarf planets:");
            foreach (var entry in task_7)
                {
                    WriteLine("{0} -> {1}", entry.Name, entry.KnownMoons);
                }
            WriteLine("Press enter to continue");
            ReadLine();
            
            var task_8 = _db.TotalMoons();
            WriteLine("All known moons:\n -> {0}", task_8);
            WriteLine("Press enter to continue");
            ReadLine();
            
            var task_9 = _db.DwarfPlanetsSortedDiameter();
            WriteLine("Dwarf planets sorted by diameter:");
            foreach (var entry in task_9)
                {
                   WriteLine("{0} -> {1}", entry.Name, entry.Diameter); 
                }
            WriteLine("Press enter to continue");
            ReadLine();
            
            var task_10 = _db.AverageMoonsPerDwarfPlanet();
            WriteLine("Average moons per Dwarf planet:\n- {0}", task_10);
            WriteLine("Press enter to continue");
            ReadLine();
            
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
            
            var task_12 = _db.TotalBodyAmount();
            WriteLine("Total planets and other bodies:\n- {0}", task_12);
            WriteLine("Press enter to continue");
            ReadLine();
            
            var task_13 = _db.ClosestNeighbourPlanets();
            WriteLine("Closest neighbouring planets:\n{0} and {1} are only {3} apart.", task_13.PlanetA,task_13.PlanetB, task_13.MeasuredDistance);
            WriteLine("Press enter to continue");
            ReadLine();
            active = false;
        }
    }
}