using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartApi.Service;
using System.Linq;

namespace ShoppingCartApi.UnitTest
{
    [TestClass]
    public class UnitTest1
    {




        [TestMethod]
        public void TestGetCustomer()
        {
            string customerName = "TestUser";

            var cartService = new ShoppingCartService();
            var task = cartService.SetCustomer(customerName);
            task.Wait();

            var customer = cartService.GetCustomer();

            Assert.AreEqual(customerName, customer.Name);

        }

        [TestMethod]
        public void TestOrderDetails()
        {
            string customerName = "TestUser";

            var cartService = new ShoppingCartService();
            var task = cartService.SetCustomer(customerName);
            task.Wait();

            var customer = cartService.GetCustomer();

            cartService.Add("P1", 5, 100);
            cartService.Add("P2", 10, 25.79M);
            cartService.Add("P3", 1, 200);
            cartService.Update("P1", 7);
            cartService.Delete("P3");
            var order = cartService.GetOrder();

            // Order lines should be 2
            Assert.AreEqual(2, order.OrderLines.Count);
            // Line 1 quantity should be 7
            Assert.AreEqual(7, order.OrderLines.ElementAt(0).Quantity);
            // Order total should be 950,79
            Assert.AreEqual(957.90M, order.Amount);
        }


        [TestMethod]
        public void TestOrderDiscount()
        {
            string customerName = "TestUser";

            var cartService = new ShoppingCartService();
            var task = cartService.SetCustomer(customerName);
            task.Wait();

            var customer = cartService.GetCustomer();
            customer.Type = Models.Strata.CustomerType.Gold;

            cartService.Add("P1", 5, 100);
            cartService.Add("P2", 10, 25);
            var order = cartService.GetOrder();


            // Order total should 727.50 after discount from 750 (3% deducted)
            Assert.AreEqual(727.50M, order.Amount);

        }
    }
}
