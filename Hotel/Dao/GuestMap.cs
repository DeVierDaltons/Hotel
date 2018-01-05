using Hotel.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Hotel.Dao
{
    public class GuestMap : ClassMapping<Guest>
    {
        public GuestMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.PhoneNumber);
            Property(x => x.EmailAdress);
            Property(x => x.ICEPhoneNumber);
            Property(x => x.Address);
            Property(x => x.PostalCode);
            Property(x => x.City);
            Property(x => x.Country);
        }
    }
}
