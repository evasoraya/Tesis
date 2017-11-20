using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;


using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows;

namespace Friends
{
    public class Database
    {

        #region Singleton
        private static Database _instance;
        public static Database getInstance()
        {
            if (_instance == null)
                _instance = new Database();
            return _instance;
        }
        #endregion

        enum DatabaseType
        {
            Postgres,
            MySQL
        };

        DatabaseType databaseType;
        const string server = "localhost";
        const string port = "5432";
        const string userId = "postgres";
        const string password = "admin";
        const string database = "tesis";
        
        NpgsqlConnection conn;
       
        public Database()
        {
            databaseType = DatabaseType.Postgres;

            string conn_command = "Server={0}; Port={1}; User Id={2}; Password={3}; Database={4}";
            conn_command = String.Format(conn_command, server, port, userId, password, database);

            conn = new NpgsqlConnection(conn_command);
            conn.Open();

        }

        public Dictionary<long, long> getBelongTo(String from_entity_id, String to_entity_id, String table_name)
        {
            switch (databaseType)
            {
                case DatabaseType.Postgres:
                    return Postgres.getInstance().getBelongTo(from_entity_id, to_entity_id, table_name);
                case DatabaseType.MySQL:
                    return MySQL.getInstance().getBelongTo(from_entity_id, to_entity_id, table_name);
                default:
                    return null;
            }
        }


        public Dictionary<long, List<long>> getIntermediateTable(String from_entity_id, String to_entity_id, String table_name)
        {
            switch (databaseType)
            {
                case DatabaseType.Postgres:
                    return Postgres.getInstance().getIntermediateTable(from_entity_id, to_entity_id, table_name);
                case DatabaseType.MySQL:
                    return MySQL.getInstance().getIntermediateTable(from_entity_id, to_entity_id, table_name);

                default:
                    return null;
            }
        }

        public List<long> loadNodeType(Entity entity)
        {

            switch (databaseType)
            {
                case DatabaseType.Postgres:
                    return Postgres.getInstance().loadNodeType(entity);
                case DatabaseType.MySQL:
                    return MySQL.getInstance().loadNodeType(entity);
                default:
                    return null;
            }
        }


        public long getLast()
        {

            string query = "SELECT LAST_INSERT_ID()";
            long id=-1;

          
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                id = dr.GetInt64(0);
                break;
            }

           
            dr.Close();
            return id;

        }

        public List<long> getActors(long movieId)
        {
            string query = "SELECT actor_id FROM film_actor WHERE film_id = " + movieId;
            /*
             *  NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            NpgsqlDataReader dr = cmd.ExecuteReader();
             */


            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }
        public int findCatByName(String name)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT category_id from Category where name LIKE @name", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "name";
            parameter.Value = name + "%";
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            int dirId = -1;

            while (dr.Read())
            {
                dirId = Int32.Parse(dr.GetString(0));
                break;
            }

            dr.Close();
            return dirId;

        }

         public int findMovieByName(String name)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT film_id from film where title LIKE @name", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "name";
            parameter.Value = name + "%";
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            int dirId = -1;

            while (dr.Read())
            {
                dirId = Int32.Parse(dr.GetString(0));
                break;
            }

            dr.Close();
            return dirId;

        }

        public List<long> findCatByFilm(long film)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT category_id from film_category where film_id LIKE @film", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "film";
            parameter.Value = film;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }
         public List<long> findfilmsByCat(long cat)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT category_id from film_category where category_id LIKE @cat", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "cat";
            parameter.Value = cat;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }

         public List<long> findfilmsByLang(long cat)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT film_id from film where language_id_id LIKE @cat", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "cat";
            parameter.Value =cat;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }

        public int findActorByName(String name, string last)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from actors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from actor where first_name LIKE @name , @last", conn);
            

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "name";
            parameter.Value = name + "%";
            cmd.Parameters.Add(parameter);

            var parameter2 = cmd.CreateParameter();
            parameter2.ParameterName = "last";
            parameter2.Value = last + "%";
            cmd.Parameters.Add(parameter2);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            int dirId = -1;

            while (dr.Read())
            {
                dirId = Int32.Parse(dr.GetString(0));
                break;
            }

            dr.Close();
            return dirId;

        }

        public long getFilmByItem(long itemId)
        {
             string query = "SELECT film_id FROM Item WHERE item_id = " + itemId;
            /*
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader(); */
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();


            long id =-1;

            while (dr.Read())
            {
               id = dr.GetInt64(0);
                break;
                
            }

            dr.Close();
            return id;
        }

        public List<long> findRentalsByName(long id)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT item_id, customer_id, staff_id from Rentals where rental_id LIKE @id", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


             List<long> answer = new List<long>();

            while (dr.Read())
            {
              
                answer.Add(dr.GetInt64(0));
                answer.Add(dr.GetInt64(1));
                answer.Add(dr.GetInt64(2));
            }

            dr.Close();
            return answer;

        }
          public List<long> findPaymentsById(long id)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT customer_id, rental_id, staff_id from Payments where payment_id LIKE @id", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


             List<long> answer = new List<long>();

            while (dr.Read())
            {
              
                answer.Add(dr.GetInt64(0));
                answer.Add(dr.GetInt64(1));
                answer.Add(dr.GetInt64(2));
            }

            dr.Close();
            return answer;

        }
        public long getStoreByItem(long itemId)
        {
            string query = "SELECT store_id FROM Item WHERE item_id = " + itemId;
            /*
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader(); */
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();


            long id =-1;

            while (dr.Read())
            {
               id = dr.GetInt64(0);
                break;
                
            }

            dr.Close();
            return id;
        }

        public List<long> findStaffsByStore(long store)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT staff_id from staff where store_id LIKE @store", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "store";
            parameter.Value = store;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }
        public List<long> findItemsByStore(long store)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT item_id from item where store_id LIKE @store", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "store";
            parameter.Value = store;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }
        public long getManagerByStore(long Id)
        {
             string query = "SELECT manager_staff_id FROM store WHERE store_id = " + Id;
            /*
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader(); */
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();


            long id =-1;

            while (dr.Read())
            {
               id = dr.GetInt64(0);
                break;
                
            }

            dr.Close();
            return id;
        }

        public List<long> findRentByCustomer(long cus)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT rent_id from rental where customer_id LIKE @cus", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "cus";
            parameter.Value = cus;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }
        public List<long> findStoresByAddress(long addr)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT store_id from store where address_id LIKE @addr", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "addr";
            parameter.Value = addr;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }

        public List<long> findAddressesBycity(long city)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT address_id from address where city_id LIKE @city", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "city";
            parameter.Value = city;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }

         public List<long> findCitiesByCountry(long country)
        {
            /*
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn); */

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT city_id from city where country_id LIKE @country", conn);
           

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "country";
            parameter.Value = country;
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();


            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }
 
 
        public NpgsqlConnection Connection
        {
            get
            {
                return conn;
            }
               
        }

    }
}
