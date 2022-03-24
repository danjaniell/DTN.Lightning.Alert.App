using DTN.Lightning.Alert.App.Services;
using System;
using Xunit;

namespace DTN.Lightning.Alert.Test
{
    public class Coordinates_Test
    {
        [Theory]
        [InlineData(-91, -181)]
        [InlineData(-91, -180)]
        [InlineData(-90, -181)]
        [InlineData(91, 181)]
        [InlineData(91, 180)]
        [InlineData(90, 181)]
        [InlineData(990, 84.12345)]
        [InlineData(84.12345, -184.12345)]
        public void InvalidLatLon_PrintInfo(double latitude, double longitude)
        {
            var isValid = CoordinatesService.ValidLatLon(latitude, longitude);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData(90, 180)]
        [InlineData(-90, -180)]
        [InlineData(-90, 180)]
        [InlineData(90, -180)]
        [InlineData(45, 77)]
        [InlineData(84.12345, 69.54321)]
        [InlineData(12.12, 0.12345)]
        [InlineData(10.5799716, -12.2895479)]
        public void ValidLatLon_PrintInfo(double latitude, double longitude)
        {
            var isValid = CoordinatesService.ValidLatLon(latitude, longitude);

            Assert.True(isValid);
        }
    }
}
