using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Parameters
{
    public abstract class PagingParameters
    {
        public string Token { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public bool Ascending { get; set; } = false;
    }
}
