// Description  QBServiceCreator
// Namespace    QBBusinessService.System
// Author       Damitha Shyamantha      Date    12/15/2017

#region UsingDirectives
using QBBusinessService.Interfaces;
using System.Threading.Tasks;
#endregion

namespace QBBusinessService.System
{
    /// <summary>
    /// service creator
    /// </summary>
    public class QBServiceCreator
    {
        #region PublicMethods
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        public TService GetService<TService>() where TService : IServiceUnit
        {
            return new QBServiceFactory().GetService<TService>();
        } 
        #endregion
    }
}
