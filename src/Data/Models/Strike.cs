namespace DTN.Lightning.Alert.App.Data.Models
{
    public class Strike
    {
        public FlashType FlashType { get; set; }
        public long StrikeTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PeakAmps { get; set; }
        public string Reserved { get; set; }
        public int IcHeight { get; set; }
        public long ReceivedTime { get; set; }
        public int NumberOfSensors { get; set; }
        public int Multiplicity { get; set; }
    }

    public enum FlashType
    {
        CloudToGround = 0,
        CloudToCloud = 1,
        HeartBeat = 9,
    }
}
