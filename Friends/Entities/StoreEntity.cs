
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
               
                Store mov = new Store(store_id: storeId, staffs : Database.getInstance().findStaffsByStore(storeId), manager_staff_id: Database.getInstance().getManagerByStore(storeId), items: Database.getInstance().findItemsByStore(storeId));
                Global.LocalStorage.SaveStore(mov);

            }


        }

        public void Add(Store mov)
        {

            
            long manager = mov.manager_staff_id;
            

            string arr = "INSERT INTO Store (  manager_staff_id) VALUES( @manager_staff_id)";
            
            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

               

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "manager_staff_id";
                parameter2.Value = manager;
                cmd.Parameters.Add(parameter2);

            

                cmd.ExecuteNonQuery();
                mov.store_id = Database.getInstance().getLast();

                Global.LocalStorage.SaveStore(mov);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Store getStoreByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var mov in Global.LocalStorage.Store_Accessor_Selector())
            {
                if (mov.store_id == ID)
                    return mov;

            }
            return null;
        }
        public void RemoveStore(int ID)
        {
            var Store = getStoreByID(ID);
          
            string arr = "DELETE FROM Store WHERE id = @ID";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Store.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
