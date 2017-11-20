
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

    public class AddressEntity : Entity
    {
        

        public AddressEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> addresses = Database.getInstance().loadNodeType(entity);

            foreach (long addressId in addresses)
            {
              
                Address mov = new Address(address_id: addressId, stores : Database.getInstance().findStoresByAddress(addressId));
                Global.LocalStorage.SaveAddress(mov);

            }


        }

    }

}
