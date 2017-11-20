
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
                Country dir = new Country(country_id: countryId);
                Global.LocalStorage.SaveCountry(dir);
            }
        }
    }
}
