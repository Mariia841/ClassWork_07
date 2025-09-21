using Microsoft.EntityFrameworkCore;

namespace Сlass04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            DisplayTracksAboveAverageListenCount(1); 
            Console.WriteLine();

            DisplayTopRated("Linkin Park"); 
            Console.WriteLine();

            SearchTracks("Faint");
            SearchTracks("I wanna be your"); 
        }

        public static void DisplayTracksAboveAverageListenCount(int albumId)
        {
            using (var dbContext = new MusicDbAppDbContext())
            {
                var album = dbContext.Albums
                                     .Include(a => a.Tracks)
                                     .FirstOrDefault(a => a.Id == albumId);

                if (album == null)
                {
                    Console.WriteLine($"Альбом з Id {albumId} не знайдено.");
                    return;
                }

                var averageListenCount = album.Tracks.Any() ? album.Tracks.Average(t => t.ListenCount) : 0;
                Console.WriteLine($"Середня кількість прослуховувань для альбома '{album.Name}': {averageListenCount:F2}");
                Console.WriteLine("Треки, кількість прослуховувань яких вища за середню:");

                var tracksAboveAverage = album.Tracks
                                             .Where(t => t.ListenCount > averageListenCount)
                                             .OrderByDescending(t => t.ListenCount)
                                             .ToList();

                if (tracksAboveAverage.Any())
                {
                    foreach (var track in tracksAboveAverage)
                    {
                        Console.WriteLine($"- '{track.Name}' (Прослуховувань: {track.ListenCount})");
                    }
                }
                else
                {
                    Console.WriteLine("Немає треків з кількістю прослуховувань вище середньої.");
                }
            }
        }

        public static void DisplayTopRated(string artistName)
        {
            using (var dbContext = new MusicDbAppDbContext())
            {
                var artist = dbContext.Bands
                                     .Include(b => b.Albums)
                                       .ThenInclude(a => a.Tracks)
                                     .FirstOrDefault(b => b.Name.ToLower() == artistName.ToLower());

                if (artist == null)
                {
                    Console.WriteLine($"Артиста '{artistName}' не знайдено.");
                    return;
                }

                Console.WriteLine($"ТОП-3 треків для артиста '{artist.Name}':");

                var topTracks = artist.Albums
                                     .SelectMany(a => a.Tracks)
                                     .Where(t => t.Rating.HasValue)
                                     .OrderByDescending(t => t.Rating)
                                     .Take(3)
                                     .ToList();

                if (topTracks.Any())
                {
                    foreach (var track in topTracks)
                    {
                        Console.WriteLine($"- '{track.Name}' (Рейтинг: {track.Rating})");
                    }
                }
                else
                {
                    Console.WriteLine("Немає треків з рейтингом для цього артиста.");
                }

                Console.WriteLine("\nТОП-3 альбомів для цього артиста:");

                var topAlbums = artist.Albums
                                     .Where(a => a.Rating.HasValue)
                                     .OrderByDescending(a => a.Rating)
                                     .Take(3)
                                     .ToList();

                if (topAlbums.Any())
                {
                    foreach (var album in topAlbums)
                    {
                        Console.WriteLine($"- '{album.Name}' (Рейтинг: {album.Rating})");
                    }
                }
                else
                {
                    Console.WriteLine("Немає альбомів з рейтингом для цього артиста.");
                }
            }
        }

        public static void SearchTracks(string searchTerm)
        {
            using (var dbContext = new MusicDbAppDbContext())
            {
                Console.WriteLine($"Результати пошуку для '{searchTerm}':");

                var foundTracks = dbContext.Tracks
                                           .Where(t => t.Name.ToLower().Contains(searchTerm.ToLower())
                                                    || (t.Lyrics != null && t.Lyrics.ToLower().Contains(searchTerm.ToLower())))
                                           .ToList();

                if (foundTracks.Any())
                {
                    foreach (var track in foundTracks)
                    {
                        Console.WriteLine($"- '{track.Name}' (Альбом: '{track.Album.Name}')");
                    }
                }
                else
                {
                    Console.WriteLine("Треків, що відповідають запиту, не знайдено.");
                }
            }
        }

        
        public static void SeedData()
        {
            using (var dbContext = new MusicDbAppDbContext())
            {
                
                if (!dbContext.Countries.Any())
                {
                    var usa = new Country { Name = "USA" };
                    var rock = new Genre { Name = "Rock" };
                    var nuMetal = new Genre { Name = "Nu Metal" };

                    var linkinPark = new Band { Name = "Linkin Park", Country = usa };
                    var coldplay = new Band { Name = "Coldplay", Country = usa };

                    var hybridTheory = new Album
                    {
                        Name = "Hybrid Theory",
                        Band = linkinPark,
                        Genre = nuMetal,
                        Rating = 5,
                        ReleaseYear = new DateTime(2000, 10, 24)
                    };

                    hybridTheory.Tracks.Add(new Track { Name = "Papercut", Duration = new TimeSpan(0, 3, 5), Rating = 5, ListenCount = 1000, Lyrics = "The sun goes down, I'm feeling small..." });
                    hybridTheory.Tracks.Add(new Track { Name = "One Step Closer", Duration = new TimeSpan(0, 2, 37), Rating = 4, ListenCount = 1500, Lyrics = "Everything you say to me..." });
                    hybridTheory.Tracks.Add(new Track { Name = "Faint", Duration = new TimeSpan(0, 2, 42), Rating = 5, ListenCount = 2000, Lyrics = "I am a little of everything" });

                    dbContext.Countries.Add(usa);
                    dbContext.Genres.Add(rock);
                    dbContext.Genres.Add(nuMetal);
                    dbContext.Bands.Add(linkinPark);
                    dbContext.Bands.Add(coldplay);
                    dbContext.Albums.Add(hybridTheory);

                    dbContext.SaveChanges();
                    Console.WriteLine("Тестові дані успішно додані до бази.");
                }
            }
        }
    }
}