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

    public class DirectorEntity : Entity
    {


        public DirectorEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> directors = Database.getInstance().loadNodeType(entity);

            foreach (long directorId in directors)
            {
                Director dir = new Director(ID: directorId);
                Global.LocalStorage.SaveDirector(dir);
            }


        }

        public void Add(Director dir)
        {

            string name = dir.Name;
            string arr = "INSERT INTO directors (name) VALUES(@Name)";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                dir.ID = Database.getInstance().findDirByName(name);

                Global.LocalStorage.SaveDirector(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public Director getDirectorByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var director in Global.LocalStorage.Director_Accessor_Selector())
            {
                if (director.ID == ID)
                    return director;

            }
            return null;
        }
        public void RemoveDirector(int ID)
        {
            var Director = getDirectorByID(ID);

            string arr = "DELETE FROM directors WHERE id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Director.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
