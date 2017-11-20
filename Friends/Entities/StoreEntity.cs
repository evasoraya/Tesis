
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

    public class StoreEntity : Entity
    {


        public StoreEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> stores = Database.getInstance().loadNodeType(entity);

            foreach (long storeId in stores)
            {
                Store mov = new Store(store_id: storeId);
                Global.LocalStorage.SaveStore(mov);
            }
        }
    }
}
