using App.CLIghtFramework.Windows;
using Services.UniverseService;
using static System.Console;

namespace App.UniverseExplorer
{
    //https://github.com/migueldeicaza/gui.cs
    
    public class MainWindow : CLIghtWindow
    {
        private bool active = true;
        private readonly IUniverseService _db;

        public MainWindow(IUniverseService db) => 
            _db = db;
        
        public override void OnWindowLoad()
        {
           // var tmpTest = _db.AverageSurfaceTemps();
           // WriteLine(tmpTest); 
           // _db.TestMethod();
            var task_2 = _db.PlanetsOrderedAlphabetical();
            foreach(var planet in task_2) WriteLine(planet.Name);
            var task_3 = _db.PlanetsTempAboveZero();
            var task_4 = _db.PlanetNameLetterConstraint();
            var task_5 = _db.PlanetsNameLengthDescending();
            foreach(var planet in task_5) WriteLine($"{planet.Name.Length} - {planet.Name}");
            var task_6 = _db.PlanetDistanceToSunAscending();
            var task_7 = _db.DwarfPlanetByMoonAmount();
            var task_8 = _db.TotalMoons();
            var task_9 = _db.DwarfPlanetsSortedDiameter();
            var task_10 = _db.AverageMoonsPerDwarfPlanet();
            var task_11 = _db.AverageSurfaceTemps();
            var task_12 = _db.TotalBodyAmount();
            var task_13 = _db.ClosestNeighbourPlanets();
            ReadLine();
            active = false;
        }
    }
}