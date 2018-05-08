using System;
using MovieApp.Entities;
using System.Linq;
using ConsoleTables;
using MovieApp.Extensions;
using MovieApp.Models;
namespace MovieApp
{
    public static class Module1Helper
    {
        internal static void SelectList()
        {
            var actors=MoviesContext.Instance.Actors.Select(a=>a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();

            var films=MoviesContext.Instance.Films.Select(b=>b.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();
        }

        internal static void SelectById()
        {
            Console.WriteLine("Enter an Actor ID");
            var actorId=Console.ReadLine().ToInt();
            var actor=MoviesContext.Instance.Actors.SingleOrDefault(a=>a.ActorId==actorId);
            if (actor==null)
            {
                Console.WriteLine($"Actor with Id {actorId} not found");
            }
            else
            {
                Console.WriteLine($"ID: {actor.ActorId} Name:{actor.FirstName} {actor.LastName}");
            }

            Console.WriteLine("Enter a Film Title");
            var title=Console.ReadLine();
            var film=MoviesContext.Instance.Films.FirstOrDefault(p=>p.Title.Contains(title));
            if (film==null)
            {
                Console.WriteLine($"Film with title {film.Title} not found");
            }
            else
            {
                Console.WriteLine($"Id:{film.FilmId} Title:{film.Title} Year: {film.ReleaseYear} Rating:{film.Rating}");
            }
        }
        internal static void CreateItem()
        {
            Console.WriteLine("Add an Actor");
            Console.WriteLine("Enter a first name");
            var firstName=Console.ReadLine();
            Console.WriteLine("Enter a last name");
            var lastName=Console.ReadLine();
            var actor=new Actor{FirstName=firstName, LastName=lastName};
            MoviesContext.Instance.Add(actor);
            MoviesContext.Instance.SaveChanges();

            var actors=MoviesContext.Instance.Actors
            .Where(a=>a.ActorId==actor.ActorId)
            .Select(a=> a.Copy<Actor, ActorModel>()); 
            ConsoleTable.From(actors).Write();
            Console.WriteLine("Add a Film");
            Console.WriteLine("Enter a Title");
            var title=Console.ReadLine();
            Console.WriteLine("Enter a description");
            var desc=Console.ReadLine();
            Console.WriteLine("Enter a release year");
            var releaseYear=Console.ReadLine().ToInt();
            Console.WriteLine("Enter a rating");
            var rating=Console.ReadLine();
            var film=new Film{Title=title, ReleaseYear=releaseYear, Description=desc, Rating=rating};
            MoviesContext.Instance.Add(film);
            MoviesContext.Instance.SaveChanges();

            var films=MoviesContext.Instance.Films
            .Where(a=>a.FilmId==film.FilmId)
            .Select(f=>f.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();
        }

        internal static void UpdateItem()
        {
            Console.WriteLine("Update an Actor");
            Console.WriteLine("Enter an Actor ID");
            var actorId=Console.ReadLine().ToInt();
            var actor=MoviesContext.Instance.Actors.SingleOrDefault(p=> p.ActorId==actorId);
            if (actor==null)
            {
                Console.WriteLine($"Actor with Id {actor.ActorId} not found");
            }
            else
            {
                ConsoleTable.From(new[] {actor.Copy<Actor, ActorModel>()})
                .Write();
                Console.WriteLine("Enter a first name");
                var firstName=Console.ReadLine().Trim();
                Console.WriteLine("Enter a last name");
                var lastName=Console.ReadLine().Trim();
                actor.FirstName=firstName;
                actor.LastName=lastName;
                MoviesContext.Instance.SaveChanges();
                var actors=MoviesContext.Instance.Actors
                .Where(a=> a.ActorId==actor.ActorId)
                .Select(a=>a.Copy<Actor, ActorModel>() );
                ConsoleTable.From(actors).Write();
            }

            Console.WriteLine("Update a Film");
            Console.WriteLine("Enter a Film ID");
            var filmId=Console.ReadLine().ToInt();
            var film=MoviesContext.Instance.Films.SingleOrDefault(a=> a.FilmId==filmId);
            if(film==null)
            {
                Console.WriteLine($"Film with Id { film.FilmId} not found");
            }
            else
            {
                ConsoleTable.From(new[] {film.Copy<Film, FilmModel>()})
                .Write();
                Console.WriteLine("Enter the title");
                var title=Console.ReadLine().Trim();
                Console.WriteLine("Enter the release year");
                var releaseYear=Console.ReadLine().ToInt();
                Console.WriteLine("Enter the rating");
                var rating=Console.ReadLine().Trim();
                if(!string.IsNullOrWhiteSpace(title) && film.Title!=title)
                {
                    film.Title=title;
                }
                if (releaseYear==0 && film.ReleaseYear!=releaseYear)
                {
                    film.ReleaseYear=releaseYear;
                }
                if (!string.IsNullOrWhiteSpace(rating) && film.Rating!=rating)
                {
                    film.Rating=rating;
                }
                MoviesContext.Instance.SaveChanges();
                var films=MoviesContext.Instance.Films
                .Where(p=> p.FilmId==filmId)
                .Select(p=> p.Copy<Film, FilmModel>());
                ConsoleTable.From(films).Write();
            }
        }
        
        internal static void DeleteItem()
        {
            Console.WriteLine("Delete an Actor");
            Console.WriteLine("Enter an Actor ID");
            var actorId=Console.ReadLine().ToInt();
            var actor=MoviesContext.Instance.Actors
            .SingleOrDefault(a=> a.ActorId==actorId);
            if (actor==null)
            {
                Console.WriteLine($"Actor with Actor Id :{actorId} not found");
            }
            else
            {
                Console.WriteLine("Existing Actor");
                WriteActors();
                MoviesContext.Instance.Remove(actor);
                MoviesContext.Instance.SaveChanges();
                Console.WriteLine("With Actor Removed");
                WriteActors();
            }
            Console.WriteLine("Delete a Film");
            Console.WriteLine("Enter a Film title");
            var title=Console.ReadLine();
            var film=MoviesContext.Instance.Films
            .FirstOrDefault(p=> p.Title.Contains(title));
            if (film==null)
            {
                Console.WriteLine($"Film with title {title} not found");
            }
            else
            {
                ConsoleTable.From(new[]{film.Copy<Film, FilmModel>()}).Write();
                Console.WriteLine("Are you sure you want to delete this film?");
                if (Console.ReadKey().Key==ConsoleKey.Y)
                {
                    MoviesContext.Instance.Remove(film);
                    MoviesContext.Instance.SaveChanges();
                    WriteFilms();
                }
                else
                {
                    Console.WriteLine("No Films deleted");
                }
            }
        }
        private static void WriteActors()
        {
            var actors=MoviesContext.Instance.Actors
            .Select(a => a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();
        }
        private static void WriteFilms()
        {
            var films=MoviesContext.Instance.Films
            .Select(a=> a.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();

        }
    }
}