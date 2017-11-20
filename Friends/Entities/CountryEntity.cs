
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

    public class CountryEntity : Entity
    {


        public CountryEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> countries = Database.getInstance().loadNodeType(entity);

            foreach (long countryId in countries)
            {
                Country dir = new Country(country_id: countryId, cities: Database.getInstance().findCitiesByCountry(countryId));
                Global.LocalStorage.SaveCountry(dir);
            }


        }

        public void Add(Country dir)
        {

            string name = dir.country;
            string arr = "INSERT INTO Country (name) VALUES(@Name)";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                dir.country_id = Database.getInstance().getLast();

                Global.LocalStorage.SaveCountry(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public Country getCountryByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var country in Global.LocalStorage.Country_Accessor_Selector())
            {
                if (country.country_id == ID)
                    return country;

            }
            return null;
        }
        public void RemoveCountry(int ID)
        {
            var country = getCountryByID(ID);

            string arr = "DELETE FROM Country WHERE country_id = @ID";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(country.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
