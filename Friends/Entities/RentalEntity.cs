
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

    public class RentalEntity : Entity
    {
        

        public RentalEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> rentals = Database.getInstance().loadNodeType(entity);

            Dictionary<long, long> rental_item = Database.getInstance().getBelongTo("rental_id", "inventory_id", "rental");
            Dictionary<long, long> rental_customer = Database.getInstance().getBelongTo("rental_id", "customer_id", "rental");

            foreach (long rentalId in rentals)
            {
                var item_id = rental_item[rentalId];
                var customer_id = rental_customer[rentalId];
                Rental mov = new Rental(rental_id: rentalId, item_id: item_id, customer_id: customer_id);
                Global.LocalStorage.SaveRental(mov);

            }
        }
    }
}
