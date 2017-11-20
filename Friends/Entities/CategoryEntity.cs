
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

    public class CategoryEntity : Entity
    {


        public CategoryEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> category = Database.getInstance().loadNodeType(entity);
            Dictionary<long, List<long>> film_categories = Database.getInstance().getIntermediateTable("category_id", "film_id", "film_category");
            List<long> f;
            foreach (long categoryId in category)
            {
                if (film_categories.ContainsKey(categoryId)) f = film_categories[categoryId];
                else f = new List<long>();

                Category dir = new Category(category_id: categoryId, films: film_categories[categoryId]);
                Global.LocalStorage.SaveCategory(dir);
            }
        }
    }
}
