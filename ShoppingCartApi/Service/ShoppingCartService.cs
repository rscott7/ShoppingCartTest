using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCartApi.Models.Strata;
using ShoppingCartApi.Service.External;
using System.Threading.Tasks;

namespace ShoppingCartApi.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        IPaymentService _paymentService;
        IStrataDataService _dataService;
        IEmailService _emailService;

        private Customer _customer;
        private IList<ShoppingCart> _lineItems = new List<ShoppingCart>();

        public ShoppingCartService()
        {
            _paymentService = new PaymentService();
            _dataService = new StrataDataService();
            _emailService = new EmailService();
        }

        public async Task SetCustomer(string customerName)
        {
            _customer = await _dataService.GetCustomer(customerName);

            if (_customer != null)
            {
                if (HttpContext.Current != null)
                {
                    var items = HttpContext.Current.Session["ShoppingCartSession" + _customer.Name];
                    if (items != null)
                    {
                        _lineItems = items as List<ShoppingCart>;
                    }
                }
            }
        }

        public Customer GetCustomer()
        {
            return _customer;
        }

        public void Add(string productCode, int quantity, decimal unitPrice)
        {
            if (_customer != null)
            {
                var item = new ShoppingCart() { Customer = _customer.Name, ProductCode = productCode, Quantity = quantity, UnitPrice = unitPrice };
                if (item.IsValid())
                {
                    _lineItems.Add(item);
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.Session["ShoppingCartSession" + _customer.Name] = _lineItems;
                    }
                }
            }
        }

        public void Update(string productCode, int quantity)
        {
            if (_customer != null)
            {
                if (quantity > 0)
                {
                    var item = _lineItems.FirstOrDefault(x => x.Customer == _customer.Name && x.ProductCode == productCode);
                    if (item != null)
                    {
                        item.Quantity = quantity;
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.Session["ShoppingCartSession" + _customer.Name] = _lineItems;
                        }
                    }
                }
                else
                {
                    Delete(productCode);
                }
            }
        }

        public void Delete(string productCode)
        {
            if (_customer != null)
            {
                var item = _lineItems.FirstOrDefault(x => x.Customer == _customer.Name && x.ProductCode == productCode);
                if (item != null)
                {
                    _lineItems.Remove(item);
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.Session["ShoppingCartSession" + _customer.Name] = _lineItems;
                    }
                }
            }
        }

        public async Task<bool> ConfirmOrder()
        {
            bool status = false;

            if (_customer != null)
            {
                var order = GetOrder();

                if (await _paymentService.Authorise(order.Customer, order.Amount))
                {
                    await _dataService.AddOrder(order);

                    // order successful
                    if (order.Id > 0)
                    {
                        status = true;
                        // Email customer;
                        _emailService.SendEmail(Properties.Resources.SystemEmail, _customer.Email, "Order Accepted", "test body");
                        // Email courier;
                        _emailService.SendEmail(Properties.Resources.SystemEmail, Properties.Resources.CourierEmail, "Order For Dispatch", "test body");

                        CheckCustomerType();

                        _lineItems = new List<ShoppingCart>();
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.Session["ShoppingCartSession" + _customer.Name] = _lineItems;
                        }
                    }
                }
            }

            return status;
        }

        public Order GetOrder()
        {
            var order = new Order();

            if (_customer != null)
            {
                order.Customer = _customer.Name;
                order.Address = _customer.Address;
                order.Date = DateTime.Now.Date;

                order.OrderLines = new List<OrderLine>();
                foreach (var item in _lineItems)
                {
                    if (item.IsValid())
                    {
                        var orderLine = new OrderLine();
                        orderLine.Quantity = item.Quantity;
                        orderLine.ProductCode = item.ProductCode;
                        orderLine.UnitPrice = item.UnitPrice;
                        order.Amount += orderLine.UnitPrice * orderLine.Quantity;
                        order.OrderLines.Add(orderLine);
                    }
                }

                if (_customer.Type == CustomerType.Gold)
                {
                    order.Amount = order.Amount * 0.97M;
                }
                else if (_customer.Type == CustomerType.Silver)
                {
                    order.Amount = order.Amount * 0.98M;
                }
            }

            return order;
        }

        private void UpdateCustomer(CustomerType type)
        {
            _customer.Type = type;
            _dataService.UpdateCustomer(_customer);
        }

        private void CheckCustomerType()
        {
            DateTime fromDate = DateTime.Now.Date.AddMonths(-12);
            var task = _dataService.GetCustomerOrders(fromDate);
            task.Wait();

            var orders = task.Result;
            var orderTotal = orders.Sum(x => x.Amount);

            if (orderTotal >= 800)
            {
                if (_customer.Type != CustomerType.Gold)
                {
                    UpdateCustomer(CustomerType.Gold);
                }
            }
            else if (orderTotal >= 500)
            {
                if (_customer.Type != CustomerType.Silver)
                {
                    UpdateCustomer(CustomerType.Silver);
                }
            }
            else
            {
                if (_customer.Type != CustomerType.Default)
                {
                    UpdateCustomer(CustomerType.Default);
                }
            }
        }

    }
}