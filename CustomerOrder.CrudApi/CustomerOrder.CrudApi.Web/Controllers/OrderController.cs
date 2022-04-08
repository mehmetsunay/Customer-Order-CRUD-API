using CustomerOrder.Crud.Api.Services;
using CustomerOrder.CrudApi.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.CrudApi.Web.Controllers
{

    [Route("api/order")]

    public class OrderController : BaseController<Order>
    {
        public OrderController(IOrderServices orderservice) : base(orderservice)
        {


        }
    }
}
