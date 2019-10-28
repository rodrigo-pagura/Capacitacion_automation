using Neoris.TAF.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.TAF.ResourceAccess.NHibernateORM
{
    public class EntityReaderAdapter<E, EM> : NHibernateAdapter
        where E: IEntity
        where EM : IMapeable, new()
    {
        private ISession session;

        public EntityReaderAdapter() : base(Assembly.GetAssembly(new EM().GetType()))
        {
            session = this.GetSession();
        }

        ~EntityReaderAdapter()
        {
            session = null;
        }

        #region Acceso a datos.-
        public List<E> ReadAll()
        {
            try
            {
                var list = session.CreateCriteria(typeof(E));
                return list.List<E>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
