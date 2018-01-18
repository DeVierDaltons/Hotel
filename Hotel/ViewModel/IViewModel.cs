using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ViewModel
{
    public interface IViewModel
    {

        /// <summary>
        /// Constructor actions should be done inside the initialize method because injection properties have not yet been set in the constructor when using Dependency injection for creation of the object
        /// </summary>
        void Initialize();
    }
}
