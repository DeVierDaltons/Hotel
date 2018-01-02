using Hotel.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Hotel.Dao
{
    public class RoomMap : ClassMapping<Room>
    {
        public RoomMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.Bookings);
            Property(x => x.RoomNumber, m => m.Length(Room.MaxLengthForRoomNames));
            Property(x => x.Beds);
            Property(x => x.Quality);
            Property(x => x.HasNiceView);
            Property(x => x.PricePerDay);
        }
    }
}
