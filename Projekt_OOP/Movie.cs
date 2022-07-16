using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_OOP
{
    public class Movie
    {
        public MovieTypes movieTypes { get; set; }
        public Movies movies { get; set; }

        public int ID
        {
            get { return movies.FilmID; }
            set { movies.FilmID = value; }
        }

        public string MovieType
        {
            get { return movieTypes.TypeName; }
            set { movieTypes.TypeName = value; }
        }

        public string MovieName
        {
            get { return movies.MovieName; }
            set { movies.MovieName = value; }
        }

        public string Movielink
        {
            get { return movies.MovieLink; }
            set { movies.MovieLink = value; }
        }

    }
}
