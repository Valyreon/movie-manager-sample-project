using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Person : BaseEntity
    {
        public string FullName => FirstName + (string.IsNullOrWhiteSpace(MiddleName) ? " " : " " + MiddleName + " ") + LastName;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Born { get; set; }
        public ICollection<Movie> StarredIn { get; set; }
        public ICollection<Movie> Directed { get; set; }
        public ICollection<Movie> Written { get; set; }

    }
}
