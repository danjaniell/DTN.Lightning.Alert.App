using DTN.Lightning.Alert.App.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DTN.Lightning.Alert.App.Services
{
    public class LightningStrikeFileService
    {
        private readonly string _path;

        public LightningStrikeFileService(string path)
        {
            _path = path;
        }

        public JsonObjWrapper FindLightningStrikes(JsonObjWrapper asset)
        {
            var errorMessages = new StringBuilder();
            var alertedStrikes = new Dictionary<object, object>();

            if (!File.Exists(_path))
            {
                return new JsonObjWrapper()
                {
                    Values = new(),
                    ErrorMessage = "Lightning strike file not found."
                };
            }

            if (new FileInfo(_path).Length == 0)
            {
                return new JsonObjWrapper()
                {
                    Values = new(),
                    ErrorMessage = "Lightning strike file is empty."
                };
            }

            int i = 1;
            foreach (string line in File.ReadLines(_path))
            {
                var strike = JsonToLightningStrike(line, i++);

                if (strike is null) { continue; }

                if (strike.FlashType == FlashType.HeartBeat) { continue; }

                if (!CoordinatesService.ValidLatLon(strike.Latitude, strike.Longitude))
                {
                    errorMessages.AppendLine("Received invalid coordinates.");
                    continue;
                }

                string quadKey = CoordinatesService.LatLonToQuadKey(strike.Latitude, strike.Longitude);

                if (asset.Values.ContainsKey(quadKey))
                {
                    if (alertedStrikes.ContainsKey(quadKey))
                    {
                        continue;
                    }
                    alertedStrikes.Add(quadKey, asset.Values[quadKey]);
                }
            }

            return new JsonObjWrapper()
            {
                Values = alertedStrikes,
                ErrorMessage = errorMessages.ToString()
            };
        }

        private static Strike JsonToLightningStrike(string json, int i)
        {
            Strike strike = null;

            try
            {
                strike = JsonConvert.DeserializeObject<Strike>(json);
            }
            catch (JsonSerializationException)
            {
                Console.WriteLine($"Encountered invalid lightning strike object at line:{i}");
            }

            return strike;
        }
    }
}
