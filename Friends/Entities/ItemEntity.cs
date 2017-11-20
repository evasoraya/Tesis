
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

        public void Add(Item dir)
        {

            long film = dir.film_id;
            long store = dir.store_id;
            string arr = "INSERT INTO Item (film_id,store_id) VALUES(@film,@store)";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "film";
                parameter.Value = film;
                cmd.Parameters.Add(parameter);

                var parameter2 = cmd.CreateParameter();
                parameter.ParameterName = "store";
                parameter2.Value = store;
                cmd.Parameters.Add(parameter2);

                cmd.ExecuteNonQuery();
                dir.item_id = Database.getInstance().getLast();

                Global.LocalStorage.SaveItem(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public Item getItemByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var item in Global.LocalStorage.Item_Accessor_Selector())
            {
                if (item.item_id == ID)
                    return item;

            }
            return null;
        }
        public void RemoveItem(int ID)
        {
            var item = getItemByID(ID);

            string arr = "DELETE FROM Item WHERE item_id = @ID";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(item.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
