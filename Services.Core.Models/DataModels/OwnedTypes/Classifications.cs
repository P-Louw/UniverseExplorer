using Services.Core.DataModels.CelestialBodies;

namespace Services.Core.DataModels
{
    public static class Classifications
    {
        enum StarType
        {
            GaintStar,
            WhiteDwarf,
            SuperGiantStar
        }

        enum PlanetType
        {
            GiantPlanet,
            IceGiant,
            MesoPlanet,
            MiniNeptune,
            Planetar,
            SuperEarth,
            SuperJupiter,
            SubEarth
        }

        enum SatelliteType
        {
            Moon,
            Asteroid
        }
    }
}