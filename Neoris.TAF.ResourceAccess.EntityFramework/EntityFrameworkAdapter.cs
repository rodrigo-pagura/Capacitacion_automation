using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.TAF.ResourceAccess.EntityFramework
{
    public abstract class EntityFrameworkAdapter<OC> where OC : DbContext, new()
    {
        protected ObjectContext GetEntityContext()
        {
            OC context = new OC();
            return ((IObjectContextAdapter)context).ObjectContext;
        }
    }
}
