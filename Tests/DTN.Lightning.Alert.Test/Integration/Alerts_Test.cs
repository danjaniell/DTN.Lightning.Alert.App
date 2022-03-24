using DTN.Lightning.Alert.App.Services;
using System;
using Xunit;

namespace DTN.Lightning.Alert.Test.Integration
{
    public class Alerts_Test
    {
        [Fact]
        public void StrikesFoundInAssets_PrintAlerts()
        {
            var assetsFileService = new AssetsFileService("./Data/assets.json");
            var assets = assetsFileService.ReadAssets();

            Assert.NotNull(assets.Values);
            Assert.NotEmpty(assets.Values);
            Assert.False(assets.HasError);

            var lightningStrikeFileService = new LightningStrikeFileService("./Data/lightning.json");
            var alerts = lightningStrikeFileService.FindLightningStrikes(assets).Values;

            Assert.NotNull(alerts.Values);
            Assert.NotEmpty(alerts.Values);
        }

        [Fact]
        public void StrikesNotFoundInAssets_PrintInfo()
        {
            var assetsFileService = new AssetsFileService("./Data/assets_no_matches.json");
            var assets = assetsFileService.ReadAssets();

            Assert.NotNull(assets.Values);
            Assert.NotEmpty(assets.Values);
            Assert.False(assets.HasError);

            var lightningStrikeFileService = new LightningStrikeFileService("./Data/lightning_no_matches.json");
            var alerts = lightningStrikeFileService.FindLightningStrikes(assets).Values;

            Assert.Empty(alerts.Values);
        }
    }
}
