using System;
using System.Collections.Generic;

namespace DatabaseManagement.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}