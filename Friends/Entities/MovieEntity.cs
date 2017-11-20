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

    public class MovieEntity : Entity
    {
        

        public MovieEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> movies = Database.getInstance().loadNodeType(entity);

            foreach (long movieId in movies)
            {
                Movie mov = new Movie(ID: movieId, Actors: Database.getInstance().getActors(movieId));
                Global.LocalStorage.SaveMovie(mov);

            }


        }

        public void Add(Movie mov)
        {

            string title = mov.Name;
            string title_year = mov.Year;
            string language_ = mov.Lang;
            long directorId = mov.Director;

            string arr = "INSERT INTO movies (movie_title,title_year,language_,directorId) VALUES(@Name, @title_year, @language_,@directorId)";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = title;
                cmd.Parameters.Add(parameter);

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "title_year";
                parameter2.Value = Double.Parse(title_year);
                cmd.Parameters.Add(parameter2);

                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "language_";
                parameter3.Value = language_;
                cmd.Parameters.Add(parameter3);

                var parameter4 = cmd.CreateParameter();
                parameter4.ParameterName = "directorId";
                parameter4.Value = directorId;
                cmd.Parameters.Add(parameter4);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.SaveMovie(mov);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Movie getMovieByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var mov in Global.LocalStorage.Movie_Accessor_Selector())
            {
                if (mov.ID == ID)
                    return mov;

            }
            return null;
        }
        public void RemoveMovie(int ID)
        {
            var Movie = getMovieByID(ID);
          
            string arr = "DELETE FROM movies WHERE id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Movie.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
