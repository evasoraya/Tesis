
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

    public class FilmEntity : Entity
    {

        public static Dictionary<long, Film> films = new Dictionary<long, Film>();

        public FilmEntity(string tableName, string entityName) : base(tableName, entityName)
        {
        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> films = Database.getInstance().loadNodeType(entity);
            Dictionary<long, List<long>> film_actors = Database.getInstance().getIntermediateTable("film_id", "actor_id", "film_actor");
            Dictionary<long, List<long>> film_categories = Database.getInstance().getIntermediateTable("film_id", "category_id", "film_category");

            foreach (long filmId in films)
            {
                Film mov = new Film(film_id: filmId, actors: film_actors[filmId], categories: film_categories[filmId]);
                FilmEntity.films[filmId] = mov;
                Global.LocalStorage.SaveFilm(mov);
            }
        }
    }
}
