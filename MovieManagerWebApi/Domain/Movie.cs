using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterPath { get; set; }
        public ushort ReleaseYear { get; set; }
        public TimeSpan Runtime { get; set; }
        public ICollection<Person> Directors { get; set; }
        public ICollection<Person> Writers { get; set; }
        public ICollection<Person> Actors { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}
