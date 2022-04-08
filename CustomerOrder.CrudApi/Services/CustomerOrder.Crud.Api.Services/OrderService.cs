using CustomerOrder.CrudApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.Crud.Api.Services
{
    public class OrderService : BaseService<Order>, IOrderServices
    {

        //consrctr ?
        public OrderService(CrudApiDbContext crudApiDbContext) : base(crudApiDbContext) { }
    }
}
