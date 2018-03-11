// Description  BaseService
// Namespace    QBBusinessService.System
// Author       Damitha Shyamantha      Date    12/18/2017

#region UsingDirectives
using Microsoft.Win32.SafeHandles;
using QBAuthManager.Interfaces;
using System;
using System.Runtime.InteropServices;
#endregion

namespace QBBusinessService.System
{
    /// <summary>
    /// base service implementation
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class BaseService : IDisposable
    {
        #region privateMembers
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        protected QBServiceCreator creator = new QBServiceCreator();
        #endregion

        #region PublicMethods        
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
