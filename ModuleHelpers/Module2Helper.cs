using System;
using System.Linq;
using ConsoleTables;
using MovieApp.Entities;
using MovieApp.Extensions;
using MovieApp.Models;

namespace MovieApp
{
    public static class Module2Helper
    {
        public static void Sort() 
        {
            var actors=MoviesContext.Instance.Actors
            .OrderBy(a=> a.LastName)
            .Select(a=>a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();

            var films=MoviesContext.Instance.Films
            .OrderBy(a=> a.Rating)
            .ThenBy(a=> a.ReleaseYear)
            .ThenBy(a=> a.Title)
            .Select(a=>a.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();
        }
        public static void SortDescending()
        {
            var actors=MoviesContext.Instance.Actors
            .OrderByDescending(a=> a.FirstName)
            .Select(a=> a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();

            var films=MoviesContext.Instance.Films
            .OrderByDescending(a=> a.Rating)
            .ThenBy(a=> a.Title)
            .Select(a=> a.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();

        }
        public static void Skip()
        {
            var films=MoviesContext.Instance.Films
            .OrderBy(a=> a.Title)
            .Skip(3)
            .Select(a=> a.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();
        }
        public static void Take()
        {
            var films=MoviesContext.Instance.Films
            .OrderBy(a=> a.Title)
            .Take(5)
            .Select(a=> a.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();
        }
        public static void Paging()
        {
            var pageSize=5;
            var pageNumber=3;
            var films=MoviesContext.Instance.Films
            .Skip((pageNumber-1)*pageSize)
            .Take(pageSize)
            .Select(a=> a.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();
        }
        public static void LinqBasics()
        {
            var serachString="ar";
            var actors=(from a in MoviesContext.Instance.Actors 
            where a.FirstName.Contains(serachString) 
            orderby a.FirstName descending  
            select a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();
        }
        public static void LambdaBasics()
        {
            var searchString="g";
            var films=MoviesContext.Instance.Films
            .Where(a=> a.Title.Contains(searchString))
            .OrderByDescending(a=> a.Title)
            .Select(a=> a.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();        }
        public static void LinqVsLambda()
        {
            var ratings = new[] {
                new { Code = "G", Name = "General Audiences"},
                new { Code = "PG", Name = "Parental Guidance Suggested"},
                new { Code = "PG-13", Name = "Parents Strongly Cautioned"},
                new { Code = "R", Name = "Restricted"},
                };

            var films = (from f in MoviesContext.Instance.Films
                        join r in ratings on f.Rating equals r.Code
                        select new { f.Title, r.Code, r.Name });
            ConsoleTable.From(films).Write();

            films = MoviesContext.Instance.Films.Join(ratings,
                        f => f.Rating,
                        r => r.Code,
                        (f, r) => new { f.Title, r.Code, r.Name });
            ConsoleTable.From(films).Write();       
        }
        public static void MigrationAddColumn()
        {
            var film=MoviesContext.Instance.Films
            .FirstOrDefault(f=> f.Title.Contains("the first avemger"));
            if (film != null)
            {
                Console.WriteLine($"updating films with is {film.FilmId} ");
                film.FilmId=124;
                MoviesContext.Instance.SaveChanges();
            }

            var films=MoviesContext.Instance.Films
            .Select(f=> f.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();
        }
        public static void MigrationAddTable()
        {
            Console.WriteLine(nameof(MigrationAddTable));
        }
        public static void CompositeKeys()
        {
            var data=new []{
                new FilmInfo{Title="Thor", ReleaseYear=2011, Rating="PG-13"},
                new FilmInfo{Title="The Avenger", ReleaseYear=2012, Rating="PG-13"},
                new FilmInfo{Title="Rogue One", ReleaseYear=2016, Rating="PG-13"}
            };
            MoviesContext.Instance.FilmInfos.AddRange(data);
            MoviesContext.Instance.SaveChanges();
            var info=MoviesContext.Instance.FilmInfos;
            ConsoleTable.From(info).Write();

        }
        private class Expression1<T>
        {
        }
    }
}