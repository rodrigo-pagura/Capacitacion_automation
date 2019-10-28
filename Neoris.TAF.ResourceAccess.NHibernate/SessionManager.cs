using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.TAF.ResourceAccess.NHibernateORM
{
    public sealed class SessionManager
    {
        private const string SESSIONKEY = "NHIBERNATE.SESSION";
        [ThreadStatic]
        private static ISession _Session; //this session is not used in web
        internal static string strConn { set; get; }
        internal static System.Reflection.Assembly Assembly2Map { get; set; }
        private readonly ISessionFactory _sessionFactory;
        #region Constructor

        #region Singleton
        public static SessionManager Instance
        {
            get
            {
                return Singleton.Instance;
            }
        }
        private class Singleton
        {
            static Singleton() { }
            internal static readonly SessionManager Instance = new SessionManager();
        }
        #endregion
        private SessionManager()
        {
            _sessionFactory = Fluently.Configure()
            .Database(OracleClientConfiguration.Oracle10.ConnectionString(strConn)
            .Driver<NHibernate.Driver.OracleDataClientDriver>())
            .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly2Map))
            .CurrentSessionContext("NHibernate.Context.ThreadStaticSessionContext, NHibernate")
            .BuildSessionFactory();
        }
        internal ISessionFactory SessionFactory { get { return _sessionFactory; } }
        public void Close()
        {
            SessionFactory.Close();
        }
        public ISession OpenConversation()
        {
            //you must set <property name="current_session_context_class">web</property> (or thread_static etc)
            ISession session = Session; //get the current session (or open one). We do this manually, not using SessionFactory.GetCurrentSession()
            //for session per conversation (otherwise remove)
            session.FlushMode = FlushMode.Never; //Only save on session.Flush() - because we need to commit on unbind in PauseConversation
            session.BeginTransaction(); //start a transaction
            CurrentSessionContext.Bind(session); //bind it
            return session;
        }
        public void EndConversation()
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);
            if (session == null) return;
            try
            {
                session.Flush();
                session.Transaction.Commit();
                session.FlushMode = FlushMode.Always;
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }
        public void ForceRollBackConversation()
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);
            if (session == null) return;
            try
            {
                session.Flush();
                session.Transaction.Rollback();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }
        public void PauseConversation()
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);
            if (session == null) return;
            try
            {
                session.Transaction.Commit(); //with flushMode=Never, this closes connections but doesn't flush
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
                throw;
            }
            //we don't close the session, and it's still in Asp SessionState
        }
        #endregion
        #region NHibernate Sessions
        public ISession OpenSession()
        {
            ISession session = SessionFactory.OpenSession();
            _Session = session;
            return session;
        }
        public ISession Session
        {
            get
            {
                ISession session = _Session;
                if (session != null && session.IsOpen)
                    return session;
                return OpenSession();
            }
        }

        public ISession CheckStatus()
        {
            return Session;
        }

        #endregion
    }
    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Debug.Write(sql.ToString());
            return sql;
        }
    }

}
