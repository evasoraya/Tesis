
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

    public class LanguageEntity : Entity
    {

        public LanguageEntity(string tableName, string entityName) : base(tableName, entityName)
        {
        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> languages = Database.getInstance().loadNodeType(entity);

            foreach (long languageId in languages)
            {
                Language dir = new Language(language_id: languageId);
                Global.LocalStorage.SaveLanguage(dir);
            }
        }
    }
}
