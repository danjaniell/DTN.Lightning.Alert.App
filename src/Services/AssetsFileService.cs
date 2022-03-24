using DTN.Lightning.Alert.App.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DTN.Lightning.Alert.App.Services
{
    public class AssetsFileService
    {
        private readonly string _path;

        public AssetsFileService(string path)
        {
            _path = path;
        }

        public JsonObjWrapper ReadAssets()
        {
            var assets = new Dictionary<object, object>();
            var errorMessages = new StringBuilder();

            if (!File.Exists(_path))
            {
                return new JsonObjWrapper()
                {
                    Values = new(),
                    ErrorMessage = "Assets file not found."
                };
            }

            string json = ReadJsonString();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new JsonObjWrapper()
                {
                    Values = new(),
                    ErrorMessage = "Assets file is empty."
                };
            }

            var jArray = ParseJsonToJArray(json);

            int i = 0;
            foreach (var asset in jArray.Children<JObject>())
            {
                string owner = asset["assetOwner"]?.ToString();
                string name = asset["assetName"]?.ToString();
                string location = asset["quadKey"]?.ToString();

                i++;
                if (owner is null || name is null || location is null)
                {
                    errorMessages.Append("Received incorrect elements reading item:").Append(i).AppendLine();
                    continue;
                }

                if (assets.ContainsKey(location))
                {
                    errorMessages.Append(location).AppendLine(" is already stored.");
                    continue;
                }
                assets.Add(location, $"{owner}:{name}");
            }

            return new JsonObjWrapper()
            {
                Values = assets,
                ErrorMessage = errorMessages.ToString()
            };
        }

        public string ReadJsonString()
        {
            string json = string.Empty;

            using (StreamReader sr = File.OpenText(_path))
            {
                json = sr.ReadToEnd();
            }

            return json;
        }

        private static JArray ParseJsonToJArray(string json)
        {
            JArray jArray = new();
            try
            {
                jArray = JArray.Parse(json);
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("Not a valid json array.");
            }
            return jArray;
        }
    }
}