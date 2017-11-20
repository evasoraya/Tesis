using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends
{
    public class Postgres
    {

        const string server = "localhost";
        const string port = "5432";
        const string userId = "postgres";
        const string password = "admin";
        const string database = "tesis";

        NpgsqlConnection conn;

        #region Singleton
        private static Postgres _instance;
        public static Postgres getInstance()
        {
            if (_instance == null)
                _instance = new Postgres();
            return _instance;
        }
        #endregion

        public Postgres()
        {
            string conn_command = "Server={0}; Port={1}; User Id={2}; Password={3}; Database={4}";
            conn_command = String.Format(conn_command, server, port, userId, password, database);

            conn = new NpgsqlConnection(conn_command);
            conn.Open();
        }

        public Dictionary<long, long> getBelongTo(String from_entity_id, String to_entity_id, String table_name)
        {
            Dictionary<long, long> answer = new Dictionary<long, long>();
            string query = "SELECT " + from_entity_id + ", " + to_entity_id + " FROM " + table_name;

            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

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
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
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

    }
}
