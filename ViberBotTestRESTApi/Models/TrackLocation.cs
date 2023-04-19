namespace ViberWalkBot.Models
{
    public class TrackLocation
    {
        public Int32 Id { get; set; }
        public string IMEI { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime DateEvent { get; set; }
        public DateTime Date_Track { get; set; }
        public int TypeSource { get; set; }
    }
}
