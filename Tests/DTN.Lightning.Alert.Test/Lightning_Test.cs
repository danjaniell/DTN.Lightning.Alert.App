using DTN.Lightning.Alert.App.Data.Models;
using DTN.Lightning.Alert.App.Services;
using System;
using Xunit;

namespace DTN.Lightning.Alert.Test
{
    public class Lightning_Test
    {
        private readonly JsonObjWrapper _asset;

        public Lightning_Test()
        {
            var assetsFileService = new AssetsFileService("./Data/assets.json");
            _asset = assetsFileService.ReadAssets();
        }

        [Fact]
        public void FileIsEmpty_PrintInfo()
        {
            var lightningStrikeFileService = new LightningStrikeFileService("./Data/empty_file.json");

            var result = lightningStrikeFileService.FindLightningStrikes(_asset);

            Assert.True(result.HasError);
            Assert.True(result.ErrorMessage.Equals("Lightning strike file is empty."));
        }

        [Fact]
        public void FileNotFound_PrintInfo()
        {
            var lightningStrikeFileService = new LightningStrikeFileService("./Data/non_existing.json");

            var result = lightningStrikeFileService.FindLightningStrikes(_asset);

            Assert.True(result.HasError);
            Assert.True(result.ErrorMessage.Equals("Lightning strike file not found."));
        }

        [Fact]
        public void HearbeatStrikes_PrintInfo()
        {
            var lightningStrikeFileService = new LightningStrikeFileService("./Data/heartbeat_strikes.json");

            var result = lightningStrikeFileService.FindLightningStrikes(_asset);

            Assert.Empty(result.Values);
        }
    }
}
