// Description  ICustomerService
// Namespace    QBBusinessService.Interfaces
// Author       Damitha Shyamantha      Date    12/15/2017

#region UsingDirectives
using System.Threading.Tasks;
#endregion

namespace QBBusinessService.Interfaces
{
    /// <summary>
    /// QB customer service interface
    /// </summary>
    /// <seealso cref="QBBusinessService.Interfaces.IServiceUnit" />
    public interface ICustomerService : IServiceUnit
    {
        /// <summary>
        /// Creates the customer asynchronous.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        Task<byte[]> CreateCustomerAsync(byte[] customer);

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        byte[] CreateCustomer(byte[] customer);

        /// <summary>
        /// Updates the customer asynchronous.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        Task<byte[]> UpdateCustomerAsync(byte[] customer);
    }
}
