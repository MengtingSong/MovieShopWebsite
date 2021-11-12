using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        private MovieService _sut;
        private static List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;
        
        // AssemblyInitialize   (once by assembly)
        //  Class1Initialize    (once by class)
        //  Class1.ctor         (before each test of the class)
        //    TestInitialize    (before each test of the class)
        //      Test1
        //    TestCleanup       (after each test of the class)
        //  Class1.Dispose      (after each test of the class)
        //
        //  Class1.ctor
        //    TestInitialize
        //      Test2
        //    TestCleanup
        //  Class1.Dispose
        //  ...
        //  Class2Initialize
        //  ...
        //  Class1Cleanup       (once by class)
        //  Class2Cleanup       (once by class)
        // AssemblyCleanup      (once by assembly)
        
        // ClassInitialize runs only on the initialization of the class where the attribute is declared.
        // In other words it won't run for every class, but just for the class that contains the ClassInitialize method.
        // ClassInitialize and ClassCleanUp are static,
        // they are only executed once even though several instances of a test class can be created by MSTest.
        // [ClassCleanup] is called after all tests from the classes are executed.
        // [OneTimeSetup] in NUnit
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avenger: Age of Ultron", Budget = 1200000},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Rings", Budget = 1200000},
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000}
            };
        }
        
        // [TestInitialize] runs before every test that is declared in the the same class.
        // [TestCleanup] is called once after each test before class is disposed.
        // [TestInitialize] -> [TestMethod] -> [TestCleanup]
        [TestInitialize]
        public void TestInitialize()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockMovieRepository.Setup(m => m.GetTop30RevenueMovies()).ReturnsAsync(_movies);
            
            // SUT - System under Test MovieServices => GetTop30RevenueMovies
            _sut = new MovieService(_mockMovieRepository.Object);
        }
        
        [TestMethod]
        public async Task TestListOfTopRevenueMoviesFromFakeData()
        {
            // AAA - Arrange, Act, Assert
            // Arrange: mock objects, data, methods, etc.
            // Act
            var movies = await _sut.GetTop30RevenueMovies();
            
            // check actual output with expected data
            //Assert
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies, typeof(IEnumerable<MovieCardResponseModel>));
            Assert.AreEqual(10, _movies.Count);
        }
    }
}