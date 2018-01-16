using Hotel.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Hotel.DataAccessObjects
{
    public class BookingMap : ClassMapping<Booking>
    {
        public BookingMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
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
                c.Lazy(false);
            });

            Property(x => x.BookingStatus);

            Set(x => x.Guests, collectionMapping =>
            {
                collectionMapping.Table("GuestBookings");
                collectionMapping.Cascade(Cascade.None);
                collectionMapping.Key(k => k.Column("BookingID"));
            },
                map => map.ManyToMany(p => p.Column("GuestID"))
            );

            Set(x => x.Rooms, collectionMapping =>
            {
                collectionMapping.Table("RoomBookings");
                collectionMapping.Cascade(Cascade.None);
                collectionMapping.Key(k => k.Column("BookingID"));
            },
                map => map.ManyToMany(p => p.Column("RoomID"))
            );
        }
    }
}
