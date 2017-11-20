
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

    public class ActorEntity : Entity
    {


        public ActorEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {

            Entity entity = (Entity)this;
            List<long> actors = Database.getInstance().loadNodeType(entity);
            Dictionary<long, List<long>> actor_films = Database.getInstance().getIntermediateTable("actor_id", "film_id", "film_actor");
            List<long> f;

            foreach (long actorId in actors){
                if (actor_films.ContainsKey(actorId)) f = actor_films[actorId];
                else f = new List<long>();

                Actor act = new Actor(actor_id: actorId, films: f);
                Global.LocalStorage.SaveActor(act);
            }


        }

    }

}
