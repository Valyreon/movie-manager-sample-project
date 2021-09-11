using System;
using System.Linq;
using DataLayer.Parameters;
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
                Value = 5,
                MovieId = godfather.Id
            };

            var reviewTwo = new Rating()
            {
                Value = 3,
                MovieId = godfather.Id
            };

            var reviewThree = new Rating()
            {
                Value = 5,
                MovieId = shawshankRedemption.Id
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

            var pageParameters = new MoviesPagingParameters()
            {
                PageNumber = 0,
                PageSize = 1
            };
            var topRatedMovie = uow.Movies.Page(pageParameters).PageItems.Single();

            Assert.AreEqual("The Shawshank Redemption", topRatedMovie.Title);
        }

        [TestMethod]
        public void GetTwoTopRatedMovieTest()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var pageParameters = new MoviesPagingParameters()
            {
                PageNumber = 0,
                PageSize = 2
            };
            var topRatedMovies = uow.Movies.Page(pageParameters).PageItems.ToList();

            Assert.AreEqual(2, topRatedMovies.Count);
            Assert.AreEqual("The Shawshank Redemption", topRatedMovies[0].Title);
            Assert.AreEqual("The Godfather", topRatedMovies[1].Title);
        }

        [TestMethod]
        public void GetDefaultTopRatedMovieTest()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var pageParameters = new MoviesPagingParameters();
            var topRatedMovies = uow.Movies.Page(pageParameters).PageItems.ToList();

            // will still return two because one movie is unrated
            Assert.AreEqual(2, topRatedMovies.Count);
            Assert.AreEqual("The Shawshank Redemption", topRatedMovies[0].Title);
            Assert.AreEqual("The Godfather", topRatedMovies[1].Title);
        }

        [TestMethod]
        public void SearchTest_4Stars()
        {
            using var uow = new UnitOfWork(new MovieDbContext(options));

            var pageParameters = new MoviesPagingParameters
            {
                Token = "4 stars"
            };
            var movie4Star = uow.Movies.Page(pageParameters).PageItems.Single();

            // godfather will have 4 average rating
            Assert.AreEqual("The Godfather", movie4Star.Title);
        }
    }
}
