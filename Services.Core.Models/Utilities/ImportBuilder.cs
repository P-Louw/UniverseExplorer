using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Core.DataModels.CelestialBodies;


namespace Services.UniverseService
{
    public static class ImportBuilder
    {
        private const string _planetTokens = "Planet";
        private const string _starTokens = "Star";
        private const string _fileNameJson = "DataSeedSerializable.json";

        public static Tuple<Star, BlockingCollection<Planet>> SeedDeserializeSunPlanets()
        {
            /*string jsonPath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
                _fileNameJson);*/
            string jsonPath = Path.Combine(
                Path.GetFullPath(Assembly.GetEntryAssembly().Location),
                _fileNameJson);
            //resultObjects = new BlockingCollection<Dictionary<string,List<>>>()
            var planets = new BlockingCollection<Planet>();
            using var reader = new StreamReader(jsonPath);
            using var jsonReader = new JsonTextReader(reader);
            var tst = JObject.ReadFrom(jsonReader);
            var planetJson = tst[_planetTokens].Value<JToken>();
            var solJson = tst[_starTokens].Value<JToken>();
            var sol = new JsonSerializer().Deserialize<Star>(solJson.CreateReader());

            Parallel.ForEach(planetJson.Children(),
                i => { planets.Add(new JsonSerializer().Deserialize<Planet>(i.CreateReader())); });

            jsonReader.SupportMultipleContent = true;
            return new Tuple<Star, BlockingCollection<Planet>>(sol, planets);
        }

        
        /// <summary>
        /// Creates JToken JObject dictionary entry, Expected json form
        /// is a object with type array per class. 
        /// </summary>
        /// <returns>"<see cref="JToken"/> list per entry.>/></returns>
        public static Dictionary<string ,JToken> SeedDataJTokens()
        {
            StringCollection NamesJObjectArrays = new StringCollection();
            NamesJObjectArrays.Add("Planet");
            NamesJObjectArrays.Add("Star");
            string jsonPath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
                _fileNameJson);
            /*string jsonPath = Path.Combine(
                Path.GetFullPath(Assembly.GetEntryAssembly().Location),
                _fileNameJson); */
            Dictionary<string,JToken> modelsData = new Dictionary<string, JToken>();
            using var reader = new StreamReader(jsonPath);
            using var jsonReader = new JsonTextReader(reader);
            var jObj = JObject.ReadFrom(jsonReader);
            foreach (string arrName in NamesJObjectArrays)
                {
                    modelsData.Add(arrName, jObj[arrName]);
                }
            

            /*var result = jObj.SelectToken("Planet")
                    .Children()
                    .OfType<JObject>()
                ;*/
            return modelsData;
        }
    }
}