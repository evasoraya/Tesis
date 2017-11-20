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

    public class CityEntity : Entity
    {


        public CityEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> cities = Database.getInstance().loadNodeType(entity);

            foreach (long cityId in cities)
            {
                City dir = new City(city_id: cityId, addresses: Database.getInstance().findAddressesBycity(cityId));
                Global.LocalStorage.SaveCity(dir);
            }


        }

        public void Add(City dir)
        {

            string name = dir.name;
            string arr = "INSERT INTO City (city) VALUES(@Name)";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                dir.city_id = Database.getInstance().getLast();

                Global.LocalStorage.SaveCity(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public City getCityByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var city in Global.LocalStorage.City_Accessor_Selector())
            {
                if (city.city_id == ID)
                    return city;

            }
            return null;
        }
        public void RemoveCity(int ID)
        {
            var City = getCityByID(ID);

            string arr = "DELETE FROM City WHERE city_id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(City.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
