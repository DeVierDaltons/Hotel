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
            ManyToOne(x => x.Guest);
            ManyToOne(x => x.Room);
            Component(x => x.BookingPeriod, c =>
            {
                // mappings for component's parts
                c.Property(x => x.StartDate);
                c.Property(x => x.EndDate);

                // mappings for component's options
                c.Access(Accessor.Property); // or Accessor.Field or Accessor.ReadOnly
                c.Class<BookingPeriod>();

                c.Insert(true);
                c.Update(true);
                c.OptimisticLock(true);
                c.Lazy(true);
            });
        }
    }
}
