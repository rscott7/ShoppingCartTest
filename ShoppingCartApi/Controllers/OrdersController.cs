using ShoppingCartApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShoppingCartApi.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        private IShoppingCartService  cartService = new ShoppingCartService();

        [Authorize]
        public async Task<IHttpActionResult> Post(string userName)
        {
            await cartService.SetCustomer(userName);

            //cartService.Add("ABC", 5, 9.99M);
            //cartService.Add("DCC", 3, 15);

            return Ok(cartService.GetOrder());
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(cartService.GetOrder());
        }


        // POST: api/Book
        [Authorize]
        //[ResponseType(typeof(TRBKHDR2))]
        public IHttpActionResult Post(string productCode, int quantity, decimal unitPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cartService.Add(productCode, quantity, unitPrice);

            return Ok();
        }

    }

    #region Helpers

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }

        public static List<Order> CreateOrders()
        {
            List<Order> OrderList = new List<Order> 
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };

            return OrderList;
        }
    }

    #endregion
}
