using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Parameters
{
    public class MoviesPagingParameters : PagingParameters
    {
        public MoviesOrderBy OrderBy { get; set; } = MoviesOrderBy.Review;
    }
}
