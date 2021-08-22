using System;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Tests
{
    [TestClass]
    public class MovieRepositoryTests
    {
        private static int dbCount = 0;
        private DbContextOptions<MovieDbContext> options;

        [TestInitialize]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieDbContext>()
                                .UseInMemoryDatabase(databaseName: $"MovieListDatabase{dbCount++}")
                                .Options;

            using var uow = new UnitOfWork(new MovieDbContext(options));

            var shawshankRedemption = new Movie()
            {
                Title = "The Shawshank Redemption",
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                ReleaseDate = new DateTime(1994, 1, 1),
            };

            var godfather = new Movie()
            {
                Title = "The Godfather",
                Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.",
                ReleaseDate = new DateTime(1972, 1, 1),
            };

            var theDarkKnight = new Movie()
            {
                Title = "The Dark Knight",
                Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                ReleaseDate = new DateTime(2008, 1, 1),
            };

            uow.Movies.Add(shawshankRedemption);
            uow.Movies.Add(theDarkKnight);
            uow.Movies.Add(godfather);

            var reviewOne = new Rating()
            {
                Value = 10,
                Movie = godfather
            };

            var reviewTwo = new Rating()
            {
                Value = 8,
                Movie = godfather
            };

            var reviewThree = new Rating()
            {
                Value = 10,
                Movie = shawshankRedemption
            };

            uow.Ratings.Add(reviewOne);
            uow.Ratings.Add(reviewTwo);
            uow.Ratings.Add(reviewThree);

            _ = uow.Commit();
        }

        [TestCleanup]
        public void Finish()
        {

        }

        [TestMethod]
        public void GetSingleTopRatedMovieTest()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var topRatedMovie = uow.Movies.GetTopRated(1).Single();

            Assert.AreEqual("The Shawshank Redemption", topRatedMovie.Title);
        }

        [TestMethod]
        public void GetTwoTopRatedMovieTest()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var topRatedMovies = uow.Movies.GetTopRated(2).ToList();

            Assert.AreEqual("The Shawshank Redemption", topRatedMovies[0].Title);
            Assert.AreEqual("The Godfather", topRatedMovies[1].Title);
        }
    }
}
