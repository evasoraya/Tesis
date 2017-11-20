using MySql.Data.MySqlClient;
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
        


        public FilmEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> films = Database.getInstance().loadNodeType(entity);

            foreach (long filmId in films)
            {
                //TODO agregar title, description, y to lo otro
                Film mov = new Film(film_id: filmId, actors: Database.getInstance().getActors(filmId), categories: Database.getInstance().findCatByFilm(filmId));
                Global.LocalStorage.SaveFilm(mov);

            }


        }

        public void Add(Film mov)
        {

            string title = mov.title;
            string release_year = mov.release_year;
            string length = mov.length;
            string description = mov.description;

            string arr = "INSERT INTO film (title, description, length, release_year) VALUES(@title, @description, @length, @description)";
            
            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "title";
                parameter.Value = title;
                cmd.Parameters.Add(parameter);

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "description";
                parameter2.Value = description;
                cmd.Parameters.Add(parameter2);

                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "length";
                parameter3.Value = length;
                cmd.Parameters.Add(parameter3);

                var parameter4 = cmd.CreateParameter();
                parameter4.ParameterName = "release_year";
                parameter4.Value = release_year;
                cmd.Parameters.Add(parameter4);


               cmd.ExecuteNonQuery();

                //Todo Insert de category

                string ar = "INSERT INTO film_actor (actor_id,film_id) VALUES(@actor,@film)";
                long last = Database.getInstance().getLast();

                foreach (var id in mov.actors)
                {
                    
                    cmd = new MySqlCommand(ar, conn);

                    var parameter5 = cmd.CreateParameter();
                    parameter5.ParameterName = "actor";
                    parameter5.Value = id;
                    cmd.Parameters.Add(parameter5);

                    var parameter6 = cmd.CreateParameter();
                    parameter6.ParameterName = "film";
                    parameter6.Value = last;
                    cmd.Parameters.Add(parameter6);

                    cmd.ExecuteNonQuery();

                }
                mov.film_id = last;

                Global.LocalStorage.SaveFilm(mov);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Film getFilmByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var mov in Global.LocalStorage.Film_Accessor_Selector())
            {
                if (mov.ID == ID)
                    return mov;

            }
            return null;
        }
        public void RemoveFilm(int ID)
        {
            var Film = getFilmByID(ID);
          
            string arr = "DELETE FROM Film WHERE id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Film.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
