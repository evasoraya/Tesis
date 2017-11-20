
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trinity;
using Trinity.Storage;

namespace Friends.Entities
{

    public class CustomerEntity : Entity
    {
        
        public CustomerEntity(string tableName, string entityName) : base(tableName, entityName)
        {
        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> customers = Database.getInstance().loadNodeType(entity);

            foreach (long customerId in customers)
            {               
                Customer mov = new Customer(customer_id: customerId);
                Global.LocalStorage.SaveCustomer(mov);
            }
        }
    }
}
