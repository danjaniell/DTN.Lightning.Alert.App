namespace DTN.Lightning.Alert.App.Services
{
    public static class CoordinatesService
    {
        private const int mapZoom = 12;

        public static string LatLonToQuadKey(double latitude, double longitude)
        {
            TileSystem.LatLongToPixelXY(latitude, longitude, mapZoom, out int pixelX, out int pixelY);
            TileSystem.PixelXYToTileXY(pixelX, pixelY, out int tileX, out int tileY);
            string quadKey = TileSystem.TileXYToQuadKey(tileX, tileY, mapZoom);

            return quadKey;
        }

        public static bool ValidLatLon(double latitude, double longitude) => LatIsValid(latitude) && LonIsValid(longitude);

        private static bool LatIsValid(double latitude) => latitude >= -90 && latitude <= 90;

        private static bool LonIsValid(double longitude) => longitude >= -180 && longitude <= 180;
    }
}
