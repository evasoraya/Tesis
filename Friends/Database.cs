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
       
        public Database()
        {
            databaseType = DatabaseType.Postgres;
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
            switch(databaseType)
            {
                case DatabaseType.Postgres:
                    return Postgres.getInstance().getLast();
                case DatabaseType.MySQL:
                    return MySQL.getInstance().getLast();
                default:
                    return -1;
            }
        }

       public void executeQuery(string query)
       {

       }
    }
}
