﻿using Services.UniverseService;
using static System.Console;

namespace App.Client
{
    public class ContextCli
    {
        private bool active = true;
        private readonly IPlanetService _db;

        public ContextCli(IPlanetService db) => 
            _db = db;
        
        public void Run()
        {
            while (active)
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
}