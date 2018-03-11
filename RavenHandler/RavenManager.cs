// Description  RavenManager
// Namespace    RavenHandler
// Author       Damitha Shyamantha      Date    12/17/2017

#region UsingDirectives
using Microsoft.Win32.SafeHandles;
using QBAuthManager.Models;
using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
#endregion

namespace RavenHandler
{
    /// <summary>
    /// raven db manager
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class RavenManager : IDisposable
    {
        #region privateMembers
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private static RavenManager _manager;
        #endregion

        #region PublicMethods
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static RavenManager Instance
        {
            get
            {
                if (_manager == null)
                    _manager = new RavenManager();
                return _manager;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="RavenManager"/> class from being created.
        /// </summary>
        private RavenManager()
        {

        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RavenManager"/> class.
        /// </summary>
        ~RavenManager()
        {
            Dispose(false);
        }
        #endregion

        #region PublicMethods        
        /// <summary>
        /// Stores the token asynchronous.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error in StoreToken</exception>
        public async Task<string> StoreTokenAsync(TokenBaerer token)
        {
            try
            {
                using (var session = SessionFactory.Instance.GetSessionAsync())
                {
                    await session.StoreAsync(token);
                    await session.SaveChangesAsync();
                    return session.Advanced.GetDocumentId(token);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error in StoreToken", exception);
            }
        }

        /// <summary>
        /// Stores the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error in StoreToken</exception>
        public string StoreToken(TokenBaerer token)
        {
            try
            {
                using (var session = SessionFactory.Instance.GetSession())
                {
                    session.Store(token);
                    session.SaveChanges();
                    return session.Advanced.GetDocumentId(token);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error in StoreToken", exception);
            }
        }

        /// <summary>
        /// Updates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">token</exception>
        /// <exception cref="System.Exception">Error in UpdateToken</exception>
        public async Task UpdateTokenAsync(TokenBaerer token)
        {
            if (token == null)
                throw new ArgumentNullException("token");
            try
            {
                using (var session = SessionFactory.Instance.GetSessionAsync())
                {
                    token.UpdatedDate = DateTime.UtcNow;
                    await session.StoreAsync(token);
                    await session.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error in UpdateToken", exception);
            }
        }

        /// <summary>
        /// Updates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <exception cref="System.ArgumentNullException">token</exception>
        /// <exception cref="System.Exception">Error in UpdateToken</exception>
        public void UpdateToken(TokenBaerer token)
        {
            if (token == null)
                throw new ArgumentNullException("token");
            try
            {
                using (var session = SessionFactory.Instance.GetSession())
                {
                    token.UpdatedDate = DateTime.UtcNow;
                    session.Store(token);
                    session.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error in UpdateToken", exception);
            }
        }

        /// <summary>
        /// Gets the token by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">id</exception>
        /// <exception cref="System.Exception">Error in GetTokenFormDb</exception>
        public TokenBaerer GetTokenById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");
            try
            {
                using (var session = SessionFactory.Instance.GetSession())
                {
                    var tokens = session.Advanced.LoadStartingWith<TokenBaerer>("TokenBaerers");
                    var meta = session.Query<TokenBaerer>().OrderByDescending(x => x.ExpiaryDate).Take(1).FirstOrDefault();
                    return session.Load<TokenBaerer>(id);
                }
            }
            catch (Exception exception)
            {

                throw new Exception("Error in GetTokenFormDb", exception);
            }
        }

        /// <summary>
        /// Gets the latest token.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error in GetLatestToken</exception>
        public async Task<TokenBaerer> GetLatestTokenAsync()
        {
            try
            {
                using (var session = SessionFactory.Instance.GetSessionAsync())
                {
                    return await session.Query<TokenBaerer>().OrderByDescending(x => x.UpdatedDate).Take(1).FirstOrDefaultAsync();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error in GetLatestToken", exception);
            }
        }

        public TokenBaerer GetLatestToken()
        {
            try
            {
                using (var session = SessionFactory.Instance.GetSession())
                {
                    return session.Query<TokenBaerer>().OrderByDescending(x => x.UpdatedDate).Take(1).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error in GetLatestToken", exception);
            }
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
                if (handle != null)
                    handle.Dispose();
            }

            disposed = true;
        }
        #endregion
    }
}
