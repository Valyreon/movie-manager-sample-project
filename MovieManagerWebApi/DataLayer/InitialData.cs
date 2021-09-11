using System;
using System.Collections.Generic;
using Domain;

namespace DataLayer
{
    public static class InitialData
    {
        private static int ratingCounter = 1;

        static InitialData()
        {

            // add 29 more users besides default
            for (var i = 2; i <= 31; ++i)
            {
                users.Add(new User { Id = i, Username = $"TestUser_{i}", Email = $"test.email{i}@default.com" });
            }

            // set same password for all users
            foreach (var user in users)
            {
                user.SetPassword("Default123");
            }

            GenerateRatings((r, id) => r.MovieId = id, movies.Count, 12);
            //GenerateRatings((r, id) => r.TVShowId = id, tvShows.Count, 8);

            for (var i = 0; i < actorNames.Count; i++)
            {
                actors.Add(new Actor { Id = i + 1, Name = actorNames[i] });
            }
        }

        private static void GenerateRatings(Action<Rating, int> setId, int numberOfMediaItems, int enoughMediaRatedNum)
        {
            var rand = new Random();
            // generate random ratings
            // each user will have between 3 and 5 rating for random media (first 12 rated media will be different)
            var enoughMediaRated = false;
            var ratedIds = new HashSet<int>();
            foreach (var user in users)
            {
                var userRatedIds = new HashSet<int>();
                var numberOfRatings = rand.Next(3) + 2;
                for (var i = 0; i < numberOfRatings; ++i)
                {
                    var mediaToRateId = rand.Next(numberOfMediaItems - 1) + 1;

                    if (!enoughMediaRated)
                    {
                        while (ratedIds.Contains(mediaToRateId) || userRatedIds.Contains(mediaToRateId))
                        {
                            mediaToRateId = rand.Next(numberOfMediaItems - 1) + 1;
                        }
                    }
                    else
                    {
                        while (userRatedIds.Contains(mediaToRateId))
                        {
                            mediaToRateId = rand.Next(numberOfMediaItems - 1) + 1;
                        }
                    }

                    if (!enoughMediaRated)
                    {
                        ratedIds.Add(mediaToRateId);
                    }

                    if (!enoughMediaRated && ratedIds.Count >= enoughMediaRatedNum)
                    {
                        enoughMediaRated = true;
                    }

                    var value = i == 0 ? (byte)5 : Convert.ToByte(rand.Next(3) + 2);
                    var rating = new Rating { Id = ratingCounter++, UserId = user.Id, Value = value };
                    setId(rating, mediaToRateId);
                    ratings.Add(rating);
                    userRatedIds.Add(mediaToRateId);
                }
            }
        }

        private static readonly List<Movie> movies = new List<Movie>()
        {
            new Movie()
            {
                Id = 1,
                Title = "The Shawshank Redemption",
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                ReleaseDate = new DateTime(1994, 1, 1),
            },
            new Movie()
            {
                Id = 2,
                Title = "The Godfather",
                Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.",
                ReleaseDate = new DateTime(1972, 1, 1),
            },
            new Movie()
            {
                Id = 3,
                Title = "The Dark Knight",
                Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                ReleaseDate = new DateTime(2008, 1, 1),
            },
            new Movie()
            {
                Id = 4,
                Title = "The Hateful Eight",
                Description = "In the dead of a Wyoming winter, a bounty hunter and his prisoner find shelter in a cabin currently inhabited by a collection of nefarious characters.",
                ReleaseDate = new DateTime(2015, 1, 1),
            },
            new Movie()
            {
                Id = 5,
                Title = "Pulp Fiction",
                Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                ReleaseDate = new DateTime(1994, 1, 1),
            },
            new Movie()
            {
                Id = 6,
                Title = "Reservoir Dogs",
                Description = "When a simple jewelry heist goes horribly wrong, the surviving criminals begin to suspect that one of them is a police informant.",
                ReleaseDate = new DateTime(1992, 1, 1),
            },
            new Movie()
            {
                Id = 7,
                Title = "Once Upon a Time... In Hollywood",
                Description = "A faded television actor and his stunt double strive to achieve fame and success in the final years of Hollywood's Golden Age in 1969 Los Angeles.",
                ReleaseDate = new DateTime(2019, 1, 1),
            },
            new Movie()
            {
                Id = 8,
                Title = "The Usual Suspects",
                Description = "A sole survivor tells of the twisty events leading up to a horrific gun battle on a boat, which began when five criminals met at a seemingly random police lineup.",
                ReleaseDate = new DateTime(1995, 1, 1),
            },
            new Movie()
            {
                Id = 9,
                Title = "Se7en",
                Description = "Two detectives, a rookie and a veteran, hunt a serial killer who uses the seven deadly sins as his motives.",
                ReleaseDate = new DateTime(1995, 1, 1),
            },
            new Movie()
            {
                Id = 10,
                Title = "The Green Mile",
                Description = "The lives of guards on Death Row are affected by one of their charges: a black man accused of child murder and rape, yet who has a mysterious gift.",
                ReleaseDate = new DateTime(1999, 1, 1),
            },
            new Movie()
            {
                Id = 11,
                Title = "The Wolf of Wall Street",
                Description = "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.",
                ReleaseDate = new DateTime(2013, 1, 1),
            },
            new Movie()
            {
                Id = 12,
                Title = "The Silence of the Lambs",
                Description = "A young F.B.I. cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims.",
                ReleaseDate = new DateTime(1991, 1, 1),
            },
            new Movie()
            {
                Id = 13,
                Title = "Goodfellas",
                Description = "The story of Henry Hill and his life in the mob, covering his relationship with his wife Karen Hill and his mob partners Jimmy Conway and Tommy DeVito in the Italian-American crime syndicate.",
                ReleaseDate = new DateTime(1990, 1, 1),
            },
            new Movie()
            {
                Id = 14,
                Title = "The Godfather: Part II",
                Description = "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.",
                ReleaseDate = new DateTime(1974, 1, 1),
            },
            new Movie()
            {
                Id = 15,
                Title = "The Godfather: Part III",
                Description = "Follows Michael Corleone, now in his 60s, as he seeks to free his family from crime and find a suitable successor to his empire.",
                ReleaseDate = new DateTime(1990, 1, 1),
            }
        };

        private static readonly List<User> users = new List<User>()
        {
            new User { Id = 1, Username = "default", Email = "default@default.com" },
        };

        private static readonly List<Rating> ratings = new List<Rating>();

        /*private static readonly List<TVShow> tvShows = new List<TVShow>()
        {
            new TVShow()
            {
                Id = 1,
                Title = "Breaking Bad",
                Description = "A high school chemistry teacher diagnosed with inoperable lung cancer turns to manufacturing and selling methamphetamine in order to secure his family's future.",
                ReleaseDate = new DateTime(2008, 1, 1),
                EndDate = new DateTime(2013,1,1),
                NumberOfSeasons = 5
            },
            new TVShow()
            {
                Id = 2,
                Title = "Peaky Blinders",
                Description = "A gangster family epic set in 1900s England, centering on a gang who sew razor blades in the peaks of their caps, and their fierce boss Tommy Shelby.",
                ReleaseDate = new DateTime(2013, 1, 1),
                NumberOfSeasons = 6
            },
            new TVShow()
            {
                Id = 3,
                Title = "Game Of Thrones",
                Description = "Nine noble families fight for control over the lands of Westeros, while an ancient enemy returns after being dormant for millennia.",
                ReleaseDate = new DateTime(2011, 1, 1),
                EndDate = new DateTime(2019,1,1),
                NumberOfSeasons = 8
            },
            new TVShow()
            {
                Id = 4,
                Title = "Stranger Things",
                Description = "When a young boy disappears, his mother, a police chief and his friends must confront terrifying supernatural forces in order to get him back.",
                ReleaseDate = new DateTime(2016, 1, 1),
                NumberOfSeasons = 4
            },
            new TVShow()
            {
                Id = 5,
                Title = "Friends",
                Description = "Follows the personal and professional lives of six twenty to thirty-something-year-old friends living in Manhattan.",
                ReleaseDate = new DateTime(1994, 1, 1),
                EndDate = new DateTime(2004,1,1),
                NumberOfSeasons = 10
            },
            new TVShow()
            {
                Id = 6,
                Title = "Money Heist",
                Description = "An unusual group of robbers attempt to carry out the most perfect robbery in Spanish history - stealing 2.4 billion euros from the Royal Mint of Spain.",
                ReleaseDate = new DateTime(2017, 1, 1),
                EndDate = new DateTime(2021,1,1),
                NumberOfSeasons = 5
            },
            new TVShow()
            {
                Id = 7,
                Title = "The Office",
                Description = "A mockumentary on a group of typical office workers, where the workday consists of ego clashes, inappropriate behavior, and tedium.",
                ReleaseDate = new DateTime(2005, 1, 1),
                EndDate = new DateTime(2013,1,1),
                NumberOfSeasons = 9
            },
            new TVShow()
            {
                Id = 8,
                Title = "True Detective",
                Description = "Seasonal anthology series in which police investigations unearth the personal and professional secrets of those involved, both within and outside the law.",
                ReleaseDate = new DateTime(2014, 1, 1),
                EndDate = new DateTime(2019,1,1),
                NumberOfSeasons = 3
            },
            new TVShow()
            {
                Id = 9,
                Title = "The Mandalorian",
                Description = "The travels of a lone bounty hunter in the outer reaches of the galaxy, far from the authority of the New Republic.",
                ReleaseDate = new DateTime(2019, 1, 1),
                NumberOfSeasons = 2
            },
            new TVShow()
            {
                Id = 10,
                Title = "The X-Files",
                Description = "Two F.B.I. Agents, Fox Mulder the believer and Dana Scully the skeptic, investigate the strange and unexplained, while hidden forces work to impede their efforts.",
                ReleaseDate = new DateTime(1993, 1, 1),
                EndDate = new DateTime(2018,1,1),
                NumberOfSeasons = 11
            },
            new TVShow()
            {
                Id = 11,
                Title = "House M.D.",
                Description = "An antisocial maverick doctor who specializes in diagnostic medicine does whatever it takes to solve puzzling cases that come his way using his crack team of doctors and his wits.",
                ReleaseDate = new DateTime(2004, 1, 1),
                EndDate = new DateTime(2012,1,1),
                NumberOfSeasons = 8
            },
            new TVShow()
            {
                Id = 12,
                Title = "Shameless",
                Description = "A scrappy, feisty, fiercely loyal Chicago family makes no apologies.",
                ReleaseDate = new DateTime(2011, 1, 1),
                EndDate = new DateTime(2021,1,1),
                NumberOfSeasons = 11
            },
            new TVShow()
            {
                Id = 13,
                Title = "Narcos",
                Description = "A chronicled look at the criminal exploits of Colombian drug lord Pablo Escobar, as well as the many other drug kingpins who plagued the country through the years.",
                ReleaseDate = new DateTime(2015, 1, 1),
                EndDate = new DateTime(2017,1,1),
                NumberOfSeasons = 3
            }
        };*/

        public static IReadOnlyDictionary<int, IEnumerable<int>> MovieActorsConnections = new Dictionary<int, IEnumerable<int>>
        {
            { 1, new int[] { 1, 2, 3 } },
            { 2, new int[] { 4, 5 } },
            { 3, new int[] { 7, 8 } },
            { 4, new int[] { 13, 17, 18 } },
            { 5, new int[] { 10, 11, 13 } },
            { 6, new int[] { 15, 16 } },
            { 7, new int[] { 12, 23, 26} },
            { 8, new int[] { 14, 15, 16 } },
            { 9, new int[] { 2, 12, 14 } },
            { 10, new int[] { 27, 28 } },
            { 11, new int[] { 23, 24, 26 } },
            { 12, new int[] { 29, 30 } },
            { 13, new int[] { 6, 19, 20} },
            { 14, new int[] { 6, 5 } },
            { 15, new int[] { 5, 21, 22 } },
        };

        public static IReadOnlyDictionary<int, IEnumerable<int>> TVShowActorsConnections = new Dictionary<int, IEnumerable<int>>
        {
            { 1, new int[] { 31, 32 } },
            { 2, new int[] { 33, 34 } },
            { 3, new int[] { 35, 36, 37 } },
            { 4, new int[] { 38, 39, 40 } },
            { 5, new int[] { 41, 42, 43 } },
            { 6, new int[] { 44, 45, 46 } },
            { 7, new int[] { 47, 48, 49 } },
            { 8, new int[] { 50, 51 } },
            { 9, new int[] { 52, 53 } },
            { 10, new int[] { 54, 55 } },
            { 11, new int[] { 58, 59 } },
            { 12, new int[] { 52, 60 } },
        };

        private static readonly IReadOnlyList<string> actorNames = new string[]
        {
            "Tim Robbins",
            "Morgan Freeman",
            "Bob Gunton",
            "Marlon Brando",
            "Al Pacino",
            "Robert De Niro",
            "Christian Bale",
            "Heath Ledger",
            "Aaron Eckhart",
            "John Travolta",
            "Uma Thurman",
            "Brad Pitt",
            "Samuel L. Jackson",
            "Kevin Spacey",
            "Harvey Keitel",
            "Michael Madsen",
            "Kurt Russell",
            "Jennifer Jason Leigh",
            "Ray Liotta",
            "Joe Pesci",
            "Diane Keaton",
            "Andy Garcia",
            "Leonardo DiCaprio",
            "Brad Pitt",
            "Jonah Hill",
            "Gabriel Byrne",
            "Margot Robbie",
            "Tom Hanks",
            "Michael Clarke Duncan",
            "Anthony Hopkins",
            "Jodie Foster",
            "Bryan Cranston",
            "Aaron Paul",
            "Cillian Murphy",
            "Helen McCrory",
            "Emilia Clarke",
            "Peter Dinklage",
            "Kit Harington",
            "Millie Bobby Brown",
            "Finn Wolfhard",
            "Winona Ryder",
            "Jennifer Aniston",
            "Matt LeBlanc",
            "Courtney Cox",
            "Ursula Corbero",
            "Alvaro Morte",
            "Itziar Ituno",
            "Steve Carell",
            "Jenna Fischer",
            "John Krasinski",
            "Matthew McConaughey",
            "Woody Harrelson",
            "Pedro Pascal",
            "Gina Carano",
            "David Duchovny",
            "Gillian Anderson",
            "Hugh Laurie",
            "Robert Sean Leonard",
            "Emmy Rossum",
            "William H. Macy",
            "Wagner Moura"
        };

        private static readonly List<Actor> actors = new List<Actor>();

        public static IReadOnlyList<Movie> Movies => movies;
        public static IReadOnlyList<Actor> Actors => actors;
        public static IReadOnlyList<Rating> Ratings => ratings;
        public static IReadOnlyList<User> Users => users;
    }
}
