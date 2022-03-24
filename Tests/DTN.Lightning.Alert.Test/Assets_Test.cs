using DTN.Lightning.Alert.App.Services;
using System;
using Xunit;

namespace DTN.Lightning.Alert.Test
{
    public class Assets_Test
    {
        [Fact]
        public void FileIsEmpty_PrintInfo()
        {
            var assetsFileService = new AssetsFileService("./Data/empty_assets.json");

            var result = assetsFileService.ReadAssets();

            Assert.True(result.HasError);
            Assert.True(result.ErrorMessage.Equals("Assets file is empty."));
        }

        [Fact]
        public void FileNotFound_PrintInfo()
        {
            var assetsFileService = new AssetsFileService("./Data/non_existing.json");

            var result = assetsFileService.ReadAssets();

            Assert.True(result.HasError);
            Assert.True(result.ErrorMessage.Equals("Assets file not found."));
        }

        [Fact]
        public void DuplicateQuadKey_PrintInfo()
        {
            var assetsFileService = new AssetsFileService("./Data/assets_with_duplicate.json");

            var result = assetsFileService.ReadAssets();

            Assert.True(result.HasError);
            Assert.Contains("023112133002 is already stored.", result.ErrorMessage);
        }

        [Fact]
        public void AssetOwnerNotFound_PrintInfo()
        {
            var assetsFileService = new AssetsFileService("./Data/no_assetOwner.json");

            var result = assetsFileService.ReadAssets();

            Assert.True(result.HasError);
            Assert.Contains("Received incorrect elements reading item:1", result.ErrorMessage);
            Assert.Contains("Received incorrect elements reading item:2", result.ErrorMessage);
        }

        [Fact]
        public void AssetNameNotFound_PrintInfo()
        {
            var assetsFileService = new AssetsFileService("./Data/no_assetName.json");

            var result = assetsFileService.ReadAssets();

            Assert.True(result.HasError);
            Assert.Contains("Received incorrect elements reading item:1", result.ErrorMessage);
            Assert.Contains("Received incorrect elements reading item:2", result.ErrorMessage);
        }

        [Fact]
        public void QuadKeyNotFound_PrintInfo()
        {
            var assetsFileService = new AssetsFileService("./Data/no_quadKey.json");

            var result = assetsFileService.ReadAssets();

            Assert.True(result.HasError);
            Assert.Contains("Received incorrect elements reading item:1", result.ErrorMessage);
            Assert.Contains("Received incorrect elements reading item:2", result.ErrorMessage);
        }

        [Fact]
        public void FileIsCorrect_DictHasValues()
        {
            var assetsFileService = new AssetsFileService("./Data/assets.json");

            var result = assetsFileService.ReadAssets();

            Assert.False(result.HasError);
            Assert.Equal(147, result.Values.Count);
        }
    }
}
