using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApi.Service.External
{
    interface IPaymentService
    {
        Task<bool> Authorise(string name, decimal amount);
    }
}
