using Neoris.TAF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.TAF.ResourceAccess.EntityFramework
{
    public abstract class EntityReaderAdapter<E, OC> : EntityFrameworkAdapter<OC>
        where E : IBusinessEntity
        where OC : DbContext, new()
    {
        protected String EntityName
        {
            get { return String.Format("{0}{1}", typeof(E).Name, "Set"); }
        }

        #region Acceso a datos.-

        public virtual List<E> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                List<E> entities = context.CreateQuery<E>(this.EntityName).ToList();
                return entities;
            }
        }

        #endregion
    }
}
