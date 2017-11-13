using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingCartApi.Service.External
{
    public class PaymentService : IPaymentService
    {

        public async Task<bool> Authorise(string name, decimal amount)
        {
            var task = new Task(null);

            await task;

            return true;
        }
    }
}