using System.Threading.Tasks;
using ShoppingCartApi.Models.Strata;
using System.Collections.Generic;
using System;

namespace ShoppingCartApi.Service
{
    public interface IStrataDataService
    {
        bool Authenticate(string userName);
        Task<Order> AddOrder(Order order);
        Task<IList<Order>> GetCustomerOrders(DateTime from);
        Task<Customer> GetCustomer(string customerName);
        Task UpdateCustomer(Customer customer);
        Task SaveChanges(StrataModel db);
    }
}