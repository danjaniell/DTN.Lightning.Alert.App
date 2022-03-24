using DTN.Lightning.Alert.App.Data.Models;
using DTN.Lightning.Alert.App.Services;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DTN.Lightning.Alert.App
{
    static class Program
    {
        static void Main(string[] args)
        {
            var assetsFileService = new AssetsFileService("./Data/assets.json");
            var assets = assetsFileService.ReadAssets();

            if (assets.HasError)
            {
                Console.WriteLine(assets.ErrorMessage);
            }

            ReadLightningStrikes(assets);

            Console.ReadKey();
        }

        private static void ReadLightningStrikes(Asset asset, string path = "./Data/lightning.json")
        {
            foreach (string line in File.ReadLines(path))
            {
                var strike = JsonConvert.DeserializeObject<Strike>(line);

                if (strike.FlashType == FlashType.HeartBeat) { continue; }

                string quadKey = CoordinatesService.LatLonToQuadKey(strike.Latitude, strike.Longitude);

                if (asset.Values.ContainsKey(quadKey))
                {
                    Console.WriteLine($"lightning alert for {asset.Values[quadKey]}");
                    asset.Values.Remove(quadKey);
                }
            }
        }
    }
}
