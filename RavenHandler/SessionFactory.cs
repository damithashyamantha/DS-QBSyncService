// Description  SessionFactory
// Namespace    RavenHandler
// Author       Damitha Shyamantha      Date    12/13/2017

#region UsingDirectives
using Microsoft.Win32.SafeHandles;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices; 
#endregion

namespace RavenHandler
{
    /// <summary>
    /// raven db session
    /// </summary>
    /// <seealso cref="RavenHandler.ISessionFactory" />
    public class SessionFactory : ISessionFactory
    {
        #region PrivateMembers
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private readonly List<IAsyncDocumentSession> _asyncSessions = new List<IAsyncDocumentSession>();

        private readonly List<IDocumentSession> _sessions = new List<IDocumentSession>();

        private static IDocumentStore _store;
        private static SessionFactory _instance;
        private static readonly object _syncRoot = new object();
        protected const string _connectionStringKey = "QBRavaenSessionDB";
        #endregion

        #region PublicProperties
        /// <summary>
        /// Gets the creator.
        /// </summary>
        public static SessionFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionFactory();
                return _instance;
            }
        }
        #endregion

        #region ProtectedProperties
        /// <summary>
        /// Gets the store.
        /// </summary>
        protected virtual IDocumentStore Store
        {
            get
            {
                if (_store == null)
                {
                    lock (_syncRoot)
                    {
                        if (_store == null)
                        {
                            var store = new DocumentStore
                            {
                                ConnectionStringName = _connectionStringKey,
                                Conventions = { DisableProfiling = true, SaveEnumsAsIntegers = true }
                            };

                            _store = store.Initialize();
                        }
                    }
                }
                return _store;
            }
        } 
        #endregion

        #region Constrouctor        
        /// <summary>
        /// Prevents a default instance of the <see cref="SessionFactory"/> class from being created.
        /// </summary>
        private SessionFactory()
        {

        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SessionFactory"/> class.
        /// </summary>
        ~SessionFactory()
        {
            Dispose(false);
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns></returns>
        public IAsyncDocumentSession GetSessionAsync()
        {
            var session = Store.OpenAsyncSession();
            session.Advanced.UseOptimisticConcurrency = false;

            _asyncSessions.Add(session);

            return session;
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns></returns>
        public IDocumentSession GetSession()
        {
            var session = Store.OpenSession();
            session.Advanced.UseOptimisticConcurrency = false;
            _sessions.Add(session);

            return session;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ProtectedMethods    
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                foreach (var session in _asyncSessions)
                {
                    if (session != null)
                        session.Dispose();
                }

                foreach (var session in _sessions)
                {
                    if (session != null)
                        session.Dispose();
                }

                if (_store != null)
                    _store.Dispose();

                if (handle != null)
                    handle.Dispose();
            }

            disposed = true;
        }

        
        #endregion
    }
}
