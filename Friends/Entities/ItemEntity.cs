
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

    public class ItemEntity : Entity
    {

        public static Dictionary<long, Film> item_film_mapper = new Dictionary<long, Film>();

        public ItemEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> items = Database.getInstance().loadNodeType(entity);

            Dictionary<long, long> item_stores = Database.getInstance().getBelongTo("inventory_id", "store_id", "inventory");
            Dictionary<long, long> item_films = Database.getInstance().getBelongTo("inventory_id", "film_id", "inventory");

            foreach (long itemId in items)
            {
                var film_id = item_films[itemId];
                var film = FilmEntity.films[film_id];
                ItemEntity.item_film_mapper[itemId] = film;

                var store_id = item_stores[itemId];
                Item dir = new Item(item_id: itemId, film_id: film_id, store_id: store_id);
                Global.LocalStorage.SaveItem(dir);
            }
        }   
    }
}
