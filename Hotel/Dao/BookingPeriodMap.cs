using Hotel.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dao
{
    public class BookingPeriodMap : ClassMapping<BookingPeriod>
    {
        public BookingPeriodMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.StartDate);
            Property(x => x.EndDate);
        }
    }
}
