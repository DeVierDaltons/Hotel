namespace Hotel.Model
{
    public class Room
    {
        public string RoomNumber { get; set; }
        public int Beds { get; set; }
        public RoomQuality Quality { get; set; }
        public bool HasNiceView { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
