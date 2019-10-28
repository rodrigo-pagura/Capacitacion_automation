using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.TAF.ResourceAccess.NHibernateORM
{
    public class NHibernateAdapter
    {
        private const String FWK_CONECTION = "FwkConection";
        public NHibernateAdapter()
        {
        }
        public NHibernateAdapter(Assembly Assembly2Map)
        {
            SessionManager.strConn = ConfigurationManager.ConnectionStrings[FWK_CONECTION].ConnectionString;
            SessionManager.Assembly2Map = Assembly2Map;
        }
        ~NHibernateAdapter()
        {
            SessionManager.Instance.Close();
        }
        public ISession GetSession()
        {
            return SessionManager.Instance.OpenSession();
        }
        public void EndConversation()
        {
            SessionManager.Instance.EndConversation();
        }
        public void OpenConversation()
        {
            SessionManager.Instance.OpenConversation();
        }
        public void PauseConversation()
        {
            SessionManager.Instance.PauseConversation();
        }
        public void ForceRollBackConversation()
        {
            SessionManager.Instance.ForceRollBackConversation();
        }

        public ISession CheckStatus()
        {
            return SessionManager.Instance.CheckStatus();
        }
    }
}
