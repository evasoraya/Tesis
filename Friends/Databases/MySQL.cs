using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends
{
    public class MySQL
    {

        const string server = "localhost";
        const string port = "3306";
        const string userId = "root";
        const string password = "";
        const string database = "sakila4";

        MySqlConnection conn;

        #region Singleton
        private static MySQL _instance;
        public static MySQL getInstance()
        {
            if (_instance == null)
                _instance = new MySQL();
            return _instance;
        }
        #endregion

        public MySQL()
        {
            string conn_command = "Server={0}; Port={1}; UID={2}; Password={3}; Database={4}";
            conn_command = String.Format(conn_command, server, port, userId, password, database);
            
            conn = new MySqlConnection(conn_command);
            conn.Open();
        }

        public Dictionary<long, long> getBelongTo(String from_entity_id, String to_entity_id, String table_name)
        {
            Dictionary<long, long> answer = new Dictionary<long, long>();
            string query = "SELECT " + from_entity_id + ", " + to_entity_id + " FROM " + table_name;

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                long from_id = dr.GetInt64(0);
                long to_id = dr.GetInt64(1);
                answer[from_id] = to_id;
            }

            dr.Close();
            return answer;
        }

        public Dictionary<long, List<long>> getIntermediateTable(String from_entity_id, String to_entity_id, String table_name)
        {
            Dictionary<long, List<long>> answer = new Dictionary<long, List<long>>();
            string query = "SELECT " + from_entity_id + ", " + to_entity_id + " FROM " + table_name;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                long from_id = dr.GetInt64(0);
                long to_id = dr.GetInt64(1);

                if (!answer.ContainsKey(from_id))
                {
                    answer[from_id] = new List<long>();
                }

                answer[from_id].Add(to_id);
            }
            dr.Close();
            return answer;
        }

        public List<long> loadNodeType(Entity entity)
        {
            string query = "SELECT " + entity.TableName + "_id FROM " + entity.TableName;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            List<long> answer = new List<long>();
            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }
            dr.Close();
            return answer;
        }



        public long getLast()
        {
            string query = "SELECT LAST_INSERT_ID()";
            long id = -1;

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                id = dr.GetInt64(0);
                break;
            }

            dr.Close();
            return id;
        }
    }
}
