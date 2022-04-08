using CustomerOrder.CrudApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.Crud.Api.Services
{
   public class CustomerService : BaseService<Customer>,ICustomerService
    {    

        //consrctr ?
        public CustomerService(CrudApiDbContext crudApiDbContext) : base(crudApiDbContext) { }
    }

}

