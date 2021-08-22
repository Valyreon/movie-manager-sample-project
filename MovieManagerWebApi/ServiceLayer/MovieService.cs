using System.Linq;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Responses;

namespace ServiceLayer
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork uow;
        private readonly IMappingService mappingService;

        public MovieService(IUnitOfWork uow, IMappingService mappingService)
        {
            this.uow = uow;
            this.mappingService = mappingService;
        }

        public MovieDetailsResponse GetMovieDetails(int id)
        {
            return mappingService.MapMovieToMovieDetailsResponse(uow.Movies.GetMovieWithLoadedData(id));
        }

        public MoviePageResponse SearchTopRatedMovies(string token, int pageNumber = 0, int itemsPerPage = 10)
        {
            return new MoviePageResponse
            {
                Items = uow.Movies.SearchTopRated(token, pageNumber, itemsPerPage).Select(mappingService.MapMovieToListItem),
                ItemsPerPage = 10,
                PageNumber = pageNumber
            };
        }
    }
}
