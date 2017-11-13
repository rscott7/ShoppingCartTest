using ShoppingCartApi.Models.Strata;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingCartApi.Service
{
    public class StrataDataService : IStrataDataService
    {
        public bool Authenticate(string userName)
        {
            using (var db = new StrataModel())
            {
                var user = db.Customers.FirstOrDefault(x => x.Name == userName);
                if (user != null)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<Order> AddOrder(Order order)
        {
            using (var db = new StrataModel())
            {
                db.Orders.Add(order);
                await SaveChanges(db);
            }

            return order;
        }

        public async Task<IList<Order>> GetCustomerOrders(DateTime from)
        {
            using (var db = new StrataModel())
            {
                return await db.Orders.Where(x => x.Date >= from).ToListAsync();
            }
        }

        public async Task<Customer> GetCustomer(string customerName)
        {
            using (var db = new StrataModel())
            {
                return await db.Customers.FirstOrDefaultAsync(x => x.Name == customerName);
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            using (var db = new StrataModel())
            {
                var existingCustomer = await db.Customers.FirstOrDefaultAsync(x => x.Name == customer.Name);
                if (existingCustomer != null)
                {
                    existingCustomer.Type = customer.Type;
                    await SaveChanges(db);
                }
            }
        }


        public async Task SaveChanges(StrataModel db)
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.StackTrace);
            }
        }
    }
}