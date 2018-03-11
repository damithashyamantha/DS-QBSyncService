// Description  BaseManager
// Namespace    QBAuthManager
// Author       Damitha Shyamantha      Date    12/04/2017

#region USingDirectives
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices; 
#endregion

namespace QBAuthManager
{
    /// <summary>
    /// base manager for qb operations
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class BaseManager : IDisposable
    {
        #region privateMembers
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
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
