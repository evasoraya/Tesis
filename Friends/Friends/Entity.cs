using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Friends
{ 
    public abstract class Entity
    {

        private string tableName;
        private string entityName;

        public string TableName { get => tableName; set => tableName = value; }
        public string EntityName { get => entityName; set => entityName = value; }

        public Entity(string tableName, string entityName)
        {
            this.tableName = tableName;
            this.entityName = entityName;
        }

        public abstract void loadData();
   

    }
}
