using App.CLIghtFramework.Windows;
using Services.UniverseService;
using static System.Console;

namespace App.UniverseExplorer
{
    public class MainWindow : CLIghtWindow
    {
        private bool active = true;
        private readonly IUniverseService _db;

        public MainWindow(IUniverseService db) => 
            _db = db;
        
        public override void OnWindowLoad()
        {
            var tmpTest = _db.AverageSurfaceTemps();
            WriteLine(tmpTest); 
            _db.TestMethod();
            var a = _db.PlanetsOrderedAlphabetical();
            foreach(var planet in a) WriteLine(planet.Name);
            var b = _db.TotalMoons();
            WriteLine(b);
            var c = _db.PlanetNameLetterConstraint();
            WriteLine(c);
            ReadLine();
            active = false;
        }
    }
}