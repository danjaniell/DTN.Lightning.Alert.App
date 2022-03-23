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
    }
}
