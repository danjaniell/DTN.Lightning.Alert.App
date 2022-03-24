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

            var lightningStrikeFileService = new LightningStrikeFileService("./Data/lightning.json");
            var alerts = lightningStrikeFileService.FindLightningStrikes(assets).Values;

            foreach(var alert in alerts.Values)
            {
                Console.WriteLine($"lightning alert for {alert}");
            }

            Console.ReadKey();
        }
    }
}
