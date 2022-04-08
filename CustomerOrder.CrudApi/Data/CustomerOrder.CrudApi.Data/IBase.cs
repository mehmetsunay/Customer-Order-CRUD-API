using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.CrudApi.Data
{
    public interface IBase
    {
            int Id { get; set; }

            DateTimeOffset Created { get; set; }

             DateTimeOffset? Updated { get; set; }

              DateTimeOffset? Deleted { get; set; }





    }
}
