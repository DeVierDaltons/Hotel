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
            Property(x => x.FirstName, m => m.Length(Guest.MaxLengthForNames));
            Property(x => x.LastName, m => m.Length(Guest.MaxLengthForNames));
            Property(x => x.PhoneNumber, m => m.Length(Guest.MaxLengthForPhoneNumbers));
            Property(x => x.EmailAdress, m => m.Length(Guest.MaxLengthForEmailAddresses));
            Property(x => x.ICEPhoneNumber, m => m.Length(Guest.MaxLengthForPhoneNumbers));
        }
    }
}
