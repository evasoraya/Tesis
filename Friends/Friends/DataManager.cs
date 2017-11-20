using Friends.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends
{
    public class DataManager
    {

        ActorEntity actorEntity;
        CategoryEntity categoryEntity;
        FilmEntity filmEntity;
        ItemEntity itemEntity;

        public DataManager()
        {
            actorEntity = new ActorEntity("actor", "Actor");
            actorEntity.loadData();

            //TODO cargar las demas entidades 

            CategoryEntity = new CategoryEntity("category", "Category");
            categoryEntity.loadData();

            ItemEntity = new ItemEntity("item", "Item");
            itemEntity.loadData();

            //.....................................................

            FilmEntity = new FilmEntity("film", "Film");
            filmEntity.loadData();
            

        }

        private static DataManager _instance;

        public static DataManager getInstance()
        {
            if (_instance == null)
                _instance = new DataManager();

            return _instance;
        }

        public void AddFilm(Film m)
        {
            filmEntity.Add(m);
        }

        public void AddActor(Actor a)
        {
            actorEntity.Add(a);
        }

        public void RemoveActor(int ID)
        {
            actorEntity.RemoveActor(ID);
        }

        public void AddCategory(Category c)
        {
           categoryEntity.Add(c);
        }

        public void RemoveCategory(int c)
        {
            categoryEntity.RemoveCategory(c);
        }
       
        public void RemoveFilm(int ID)
        {
            movieEntity.RemoveFilm(ID);
        }
    }
}
