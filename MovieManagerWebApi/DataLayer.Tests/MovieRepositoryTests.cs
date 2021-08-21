using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Tests
{
    [TestClass]
    public class MovieRepositoryTests
    {
        private DbContextOptions<MovieDbContext> options;

        [TestInitialize]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieDbContext>()
                                .UseInMemoryDatabase(databaseName: "MovieListDatabase")
                                .Options;

            using var uow = new UnitOfWork(new MovieDbContext(options));

            var shawshankRedemption = new Movie()
            {
                Title = "The Shawshank Redemption",
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                ReleaseYear = 1994,
                Runtime = new System.TimeSpan(2, 22, 0)
            };

            var godfather = new Movie()
            {
                Title = "The Godfather",
                Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.",
                ReleaseYear = 1972,
                Runtime = new System.TimeSpan(2, 55, 0)
            };

            var theDarkKnight = new Movie()
            {
                Title = "The Dark Knight",
                Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                ReleaseYear = 2008,
                Runtime = new System.TimeSpan(2, 32, 0)
            };

            uow.Movies.Add(shawshankRedemption);
            uow.Movies.Add(theDarkKnight);
            uow.Movies.Add(godfather);

            _ = uow.Commit();
        }

        [TestCleanup]
        public void Finish()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));
            uow.Movies.RemoveRange(uow.Movies.GetAll());
            uow.Commit();
        }

        [TestMethod]
        public void GetMoviesPage_PageCountLargerThanTotalMovies()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var page = uow.Movies.GetPage(null, null, true, 0, 5).ToList();

            // assert we get the page count
            Assert.AreEqual(3, page.Count);
        }

        [TestMethod]
        public void GetMoviesPage_SortedByRelease_Ascending()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var page = uow.Movies.GetPage(null, "release", true, 0, 2).ToList();

            // assert we get the page count
            Assert.AreEqual(2, page.Count);

            // test order
            for (var i = 0; i < page.Count; i++)
            {
                if (i < page.Count - 1)
                {
                    Assert.IsTrue(page[i].ReleaseYear < page[i + 1].ReleaseYear);
                }
            }
        }

        [TestMethod]
        public void GetMoviesPage_SortedByRelease_Descending()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var page = uow.Movies.GetPage(null, "release", false, 0, 2).ToList();

            // assert we get the page count
            Assert.AreEqual(2, page.Count);

            // test order
            for (var i = 0; i < page.Count; i++)
            {
                if (i < page.Count - 1)
                {
                    Assert.IsTrue(page[i].ReleaseYear > page[i + 1].ReleaseYear);
                }
            }
        }

        [TestMethod]
        public void GetMoviesPage_SortedByTitle_Ascending()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var page = uow.Movies.GetPage(null, "title", true, 0, 2).ToList();

            // assert we get the page count
            Assert.AreEqual(2, page.Count);

            // test order
            for (var i = 0; i < page.Count; i++)
            {
                if (i < page.Count - 1)
                {
                    Assert.IsTrue(string.Compare(page[i].Title, page[i + 1].Title) < 0);
                }
            }
        }

        [TestMethod]
        public void GetMoviesPage_SortedByDuration_Ascending()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var page = uow.Movies.GetPage(null, "duration", true, 0, 2).ToList();

            // assert we get the page count
            Assert.AreEqual(2, page.Count);

            // test order
            for (var i = 0; i < page.Count; i++)
            {
                if (i < page.Count - 1)
                {
                    Assert.IsTrue(page[i].Runtime < page[i + 1].Runtime);
                }
            }
        }
    }
}
