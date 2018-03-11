// Description  QbServiceFactory
// Namespace    QBBusinessService.System
// Author       Damitha Shyamantha      Date    12/15/2017

#region UsingDirectives
using QBBusinessService.Interfaces;
using System;
using System.Collections.Generic; 
#endregion

namespace QBBusinessService.System
{
    /// <summary>
    /// Service factory for quickbook facade
    /// </summary>
    public class QBServiceFactory
    {
        #region PublicMethods        
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        /// <exception cref="System.Exception">Service not found</exception>
        public TService GetService<TService>() where TService : IServiceUnit
        {
            var type = typeof(TService);
            var typeDictionary = GetServiceDictionary();
            var service = default(TService);

            switch (typeDictionary[type])
            {
                case 1:
                    service = (TService)(AuthService.Instance as IAuthService);
                    break;
            }

            if (service == null)
                throw new Exception("Service not found");

            return service;
        }
        #endregion

        #region PrivateMethods        
        /// <summary>
        /// Gets the service dictionary.
        /// </summary>
        /// <returns></returns>
        private Dictionary<Type, int> GetServiceDictionary()
        {
            var dictionary = new Dictionary<Type, int>
            {
                {typeof(IAuthService), 1},
                {typeof(ICustomerService), 2}
            };

            return dictionary;
        } 
        #endregion
    }
}
