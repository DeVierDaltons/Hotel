using Hotel.Data;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Hotel.DataAccessObjects
{
    public class RoomMap : ClassMapping<Room>
    {
        public RoomMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.RoomNumber);
            Property(x => x.Beds);
            Property(x => x.Quality);
            Property(x => x.HasNiceView);
            Property(x => x.PricePerDay);
        }
    }
}
