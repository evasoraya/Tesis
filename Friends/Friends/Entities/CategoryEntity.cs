using MySql.Data.MySqlClient;
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

    public class CategoryEntity : Entity
    {


        public CategoryEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> category = Database.getInstance().loadNodeType(entity);

            foreach (long categoryId in category)
            {
                Category dir = new Category(category_id: categoryId, films: Database.getInstance().findfilmsByCat(categoryId));
                Global.LocalStorage.SaveCategory(dir);
            }


        }

        public void Add(Category dir)
        {

            string name = dir.name;
            string arr = "INSERT INTO category (name) VALUES(@Name)";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                dir.category_id = Database.getInstance().findCatByName(name);

                Global.LocalStorage.SaveCategory(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public Category getCategoryByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var category in Global.LocalStorage.Category_Accessor_Selector())
            {
                if (category.category_id == ID)
                    return category;

            }
            return null;
        }
        public void RemoveCategory(int ID)
        {
            var category = getCategoryByID(ID);

            string arr = "DELETE FROM category WHERE category_id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(category.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
