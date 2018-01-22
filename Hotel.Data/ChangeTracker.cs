using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public class ChangeTracker : IIdentifiable
    {
        public virtual Guid Id { get; set; }
        public virtual Guid ChangedObject { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual Type ChangedObjectType { get; set; }
    }
}
