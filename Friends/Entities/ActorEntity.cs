
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

        public void Add(Actor act)
        {

            string name = act.first_name;
            string lastName = act.last_name;
            string arr = "INSERT INTO actor (first_name,last_name) VALUES(@Name,@lastName)";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "lastName";
                parameter2.Value = lastName;
                cmd.Parameters.Add(parameter2);

                cmd.ExecuteNonQuery();

                act.actor_id = Database.getInstance().findActorByName(name,lastName);

                Global.LocalStorage.SaveActor(act);    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Actor getActorByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.actor_id == ID
                          select node;
                          */

            foreach(var actor in Global.LocalStorage.Actor_Accessor_Selector())
            {
                if (actor.actor_id == ID)
                    return actor;

            }
            return null;
        }
        public void RemoveActor(int ID)
        {
            var Actor = getActorByID(ID);

            string arr = "DELETE FROM Actor WHERE actor_id = @ID";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;
                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Actor.CellID);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
