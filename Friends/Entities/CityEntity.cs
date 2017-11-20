
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
                City dir = new City(city_id: cityId);
                Global.LocalStorage.SaveCity(dir);
            }
        }
    }
}
