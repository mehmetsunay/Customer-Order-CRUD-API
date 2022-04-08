using CustomerOrder.Crud.Api.Services;
using CustomerOrder.CrudApi.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.CrudApi.Web.Controllers
{


    [Route("api/customer")]

    public class CustomerController : BaseController<Customer>
    {
        public CustomerController(ICustomerService customerservice):base(customerservice)
        {


        }
    }
}
