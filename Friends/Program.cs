// Graph Engine
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
using Friends.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Trinity;
using Trinity.Storage;

namespace Friends
{
    class Friends
    {

        public unsafe static void Main(string[] args)
        {

            ActorEntity actor = new ActorEntity("actor", "Actor");
            actor.loadData();

            FilmEntity film = new FilmEntity("film", "Film");
            film.loadData();

            CategoryEntity category = new CategoryEntity("category", "Category");
            category.loadData();

            RentalEntity rental = new RentalEntity("rental", "Rental");
            rental.loadData();

            ItemEntity item = new ItemEntity("inventory", "Inventory");
            item.loadData();


            do
            {
                showMenu();

                ConsoleKey key = Console.ReadKey().Key;
               
                if (key == ConsoleKey.Escape) break;
                
                /*
                if(key == ConsoleKey.A)
                {
                    addActor();
                }

                if(key == ConsoleKey.B)
                {
                    addCategory();
                }

                if(key == ConsoleKey.C)
                {
                    addFilm();
                }

                if(key == ConsoleKey.Q)
                {
                    removeActor();
                }
                if (key == ConsoleKey.W)
                {
                    removeCategory();
                }

                if (key == ConsoleKey.E)
                {
                    removeFilm();
                }
                */
                /*
                if( key == ConsoleKey.T)
                {
                    queryMagico();
                }
                */
                if( key == ConsoleKey.Z){
                    solveFilmCountPerCategory();
                }

                if (key == ConsoleKey.X)
                {
                    solveFilmPerCategorySorted();
                }

                if (key == ConsoleKey.C)
                {
                    solveFilmPerCategoryHaving();
                }

                if (key == ConsoleKey.V)
                {
                    solveFilmPerCategoryHavingSorted();
                }

                if( key == ConsoleKey.B )
                {
                    solveActorFilmCountPerCategory();
                }

                if( key == ConsoleKey.N)
                {
                    solveActorRents();
                }
            } while (true);
            
        }

        private static void solveActorRents()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Dictionary<long, long> actorCount = new Dictionary<long, long>();

            foreach (var rental in Global.LocalStorage.Rental_Accessor_Selector())
            {

                var film = ItemEntity.item_film_mapper[rental.item_id];

                foreach (var actor in film.actors)
                {

                    if(!actorCount.ContainsKey(actor))
                    {
                        actorCount[actor] = 0;
                    }
                    actorCount[actor]++;
                }
            }

            var myList = actorCount.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            for(int i = 1; i <= 3; i++)
            {
                int j = myList.Count - i;
                Console.WriteLine(myList[j].Key + " " + myList[j].Value);
            }


            stopwatch.Stop();


            Console.WriteLine("Tiempo tomado = " + stopwatch.ElapsedMilliseconds);


        }

        private static void solveActorFilmCountPerCategory()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            
            foreach (var actor in Global.LocalStorage.Actor_Accessor_Selector())
            {
                Dictionary<long, long> categoryCount = new Dictionary<long, long>();
                foreach (var movie in actor.films)
                {

                    var film = FilmEntity.films[movie];
                    foreach (var category in film.categories)
                    {
                        if(!categoryCount.ContainsKey(category))
                        {
                            categoryCount[category] = 0;
                        }
                        categoryCount[category]++;
                    }                    
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Tiempo tomado = " + stopwatch.ElapsedMilliseconds);
        }


        private static void solveFilmPerCategorySorted()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Dictionary<long, long> categoryCount = new Dictionary<long, long>();
            foreach (var movie in Global.LocalStorage.Film_Accessor_Selector())
            {
                foreach (var category in movie.categories)
                {
                    if (!categoryCount.ContainsKey(category))
                    {
                        categoryCount[category] = 0;
                    }
                    categoryCount[category]++;
                }
            }

            stopwatch.Stop();

            var myList = categoryCount.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            myList.ForEach(pair => {
                Console.WriteLine(pair.Key + " " + pair.Value);
            });

            Console.WriteLine("Tiempo tomado = " + stopwatch.ElapsedMilliseconds);
        }

        private static void solveFilmPerCategoryHavingSorted()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Dictionary<long, long> categoryCount = new Dictionary<long, long>();
            Dictionary<long, long> categoryMax = new Dictionary<long, long>();
            foreach (var movie in Global.LocalStorage.Film_Accessor_Selector())
            {
                foreach (var category in movie.categories)
                {
                    if (!categoryCount.ContainsKey(category))
                    {
                        categoryCount[category] = 0;
                        categoryMax[category] = 0;
                    }
                    categoryCount[category]++;
                    categoryMax[category] = 159;
                }
            }

            stopwatch.Stop();

            var myList = categoryMax.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            myList.ForEach(pair => {
                long category = pair.Key;
                Console.WriteLine(category + " " + categoryCount[category] + " " + categoryMax[category]);
            });


            Console.WriteLine("Tiempo tomado = " + stopwatch.ElapsedMilliseconds);
        }

        private static void solveFilmCountPerCategory()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Dictionary<long, long> categoryCount = new Dictionary<long, long>();
            foreach (var movie in Global.LocalStorage.Film_Accessor_Selector())
            {
                foreach(var category in movie.categories)
                {
                    if( !categoryCount.ContainsKey(category))
                    {
                        categoryCount[category] = 0;
                    }
                    categoryCount[category]++;
                }
            }

            stopwatch.Stop();
            
            foreach(var category in categoryCount.Keys)
            {
                Console.WriteLine(category + " " + categoryCount[category]);
            }
            Console.WriteLine("Tiempo tomado = " + stopwatch.ElapsedMilliseconds);
        }

        private static void solveFilmPerCategoryHaving()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Dictionary<long, long> categoryCount = new Dictionary<long, long>();
            Dictionary<long, long> categoryMax = new Dictionary<long, long>();
            foreach (var movie in Global.LocalStorage.Film_Accessor_Selector())
            {
                foreach (var category in movie.categories)
                {
                    if (!categoryCount.ContainsKey(category))
                    {
                        categoryCount[category] = 0;
                        categoryMax[category] = 0;
                    }
                    categoryCount[category]++;
                    categoryMax[category] = 159;
                }
            }

            stopwatch.Stop();

            foreach (var category in categoryCount.Keys)
            {
                Console.WriteLine(category + " " + categoryCount[category] + " " + categoryMax[category]);
            }
            Console.WriteLine("Tiempo tomado = " + stopwatch.ElapsedMilliseconds);
        }

        private static void queryMagico()
        {

            long answerl = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var movie in Global.LocalStorage.Film_Accessor_Selector())
            {
                foreach(var actor in movie.actors)
                {
                    foreach(var actor2 in movie.actors)
                    {
                        answerl++;
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Respuesta = " + answerl);
            Console.WriteLine("Tiempo tomado = " + stopwatch.ElapsedMilliseconds);

        }

       
        private static void showMenu()
        {
            Console.WriteLine("A. Agregar un Actor");
            Console.WriteLine("B. Agregar un Director");
            Console.WriteLine("C. Agregar una Pelicula");


            Console.WriteLine("Q. Borrar un Actor");
            Console.WriteLine("W. Borrar un Director");
            Console.WriteLine("E. Borrar una Pelicula");

            Console.WriteLine("T. QUERY MAGICO");

        }
        
    }

}

