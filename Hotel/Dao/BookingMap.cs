using Hotel.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Hotel.Dao
{
    public class BookingMap : ClassMapping<Booking>
    {
        public BookingMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            ManyToOne(x => x.Guest, x =>
            {
                x.PropertyRef("Id");
                x.Column("Guest");
            });
            ManyToOne(x => x.Room, x =>
            {
                x.PropertyRef("Id");
                x.Column("Room");
            });
            OneToOne(x => x.BookingPeriod, x =>
            {            });
        }
    }
}
