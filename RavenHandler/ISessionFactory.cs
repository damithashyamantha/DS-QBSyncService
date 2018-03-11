// Description  ISessionFactory
// Namespace    RavenHandler
// Author       Damitha Shyamantha      Date    12/13/2017

#region UsingDirectives
using Raven.Client;
using System; 
#endregion

namespace RavenHandler
{
    /// <summary>
    /// raven session factory interface
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    interface ISessionFactory : IDisposable
    {
        /// <summary>
        /// Gets the session asynchronous.
        /// </summary>
        /// <returns></returns>
        IAsyncDocumentSession GetSessionAsync();

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns></returns>
        IDocumentSession GetSession();
    }
}
