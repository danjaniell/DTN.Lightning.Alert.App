using DTN.Lightning.Alert.App.Data.Models;
using DTN.Lightning.Alert.App.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace DTN.Lightning.Alert.App
{
    static class Program
    {
        static void Main(string[] args)
        {
            var assets = new Dictionary<object, object>();

            using (StreamReader sr = File.OpenText("./Data/assets.json"))
            {
                var json = sr.ReadToEnd();
                var jArray = JArray.Parse(json);

                foreach (var asset in jArray.Children<JObject>())
                {
                    string owner = asset["assetOwner"].ToString();
                    string name = asset["assetName"].ToString();
                    string location = asset["quadKey"].ToString();

                    assets.Add(location, $"{owner}:{name}");
                }
            }
            foreach (string line in File.ReadLines("./Data/lightning.json"))
            {
                var strike = JsonConvert.DeserializeObject<Strike>(line);

                if (strike.FlashType == FlashType.HeartBeat) { continue; }

                string quadKey = CoordinatesService.LatLonToQuadKey(strike.Latitude, strike.Longitude);

                if (assets.ContainsKey(quadKey))
                {
                    Console.WriteLine($"lightning alert for {assets[quadKey]}");
                    assets.Remove(quadKey);
                }
            }

            Console.ReadKey();
        }
    }
}
