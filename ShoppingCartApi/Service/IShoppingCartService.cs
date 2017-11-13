using ShoppingCartApi.Models.Strata;
using System.Threading.Tasks;

namespace ShoppingCartApi.Service
{
    public interface IShoppingCartService
    {
        Task SetCustomer(string customerName);
        Customer GetCustomer();
        void Add(string productCode, int quantity, decimal unitPrice);
        Order GetOrder();
        Task<bool> ConfirmOrder();
        void Delete(string productCode);
        void Update(string productCode, int quantity);
    }
}