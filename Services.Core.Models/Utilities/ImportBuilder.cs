using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Core.DataModels.CelestialBodies;


namespace Services.UniverseService
{
    public static class ImportBuilder
    {

        public static Tuple<Star, BlockingCollection<Planet>> SeedDeserializeSunPlanets()
        {
            string jsonPath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
                "DataSeedSerializable.json");
            //resultObjects = new BlockingCollection<Dictionary<string,List<>>>()
            var planets = new BlockingCollection<Planet>();
            using var reader = new StreamReader(jsonPath);
            using var jsonReader = new JsonTextReader(reader);
            var tst = JObject.ReadFrom(jsonReader);
            var planetJson = tst["Planet"].Value<JToken>();
            var solJson = tst["Star"].Value<JToken>();
            var sol = new JsonSerializer().Deserialize<Star>(solJson.CreateReader());
            
            Parallel.ForEach(planetJson.Children(), i =>
            {
                planets.Add(new JsonSerializer().Deserialize<Planet>(i.CreateReader()));
            });

            jsonReader.SupportMultipleContent = true;
            return new Tuple<Star,BlockingCollection<Planet>>(sol ,planets);
        }
        
        /// <summary>
        /// Creates JToken JObject dictionary entry, Expected json form
        /// is a object with type array per class. 
        /// </summary>
        /// <returns>"<see cref="JToken"/> list per entry.>/></returns>
        public static IEnumerable<JToken> SeedDataJTokens()
        {
            string jsonPath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
                "DataSeedSerializable.json");
            using var reader = new StreamReader(jsonPath);
            using var jsonReader = new JsonTextReader(reader);
            
            
            var jObj = JObject.ReadFrom(jsonReader);
            var result = jObj.SelectToken("Planet")
                .Children()
                .OfType<JObject>()
                ;
            return result;
        }
    }
}