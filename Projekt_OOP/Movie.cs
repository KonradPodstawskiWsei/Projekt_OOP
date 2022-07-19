namespace Projekt_OOP
{
    public class Movie
    {
        /// <summary>
        /// reprezentacja w kodzie relacji do tabeli movieTypes
        /// </summary>
        public MovieTypes movieTypes { get; set; }

        /// <summary>
        /// reprezentacja w kodzie relacji do tabeli movies
        /// </summary>
        public Movies movies { get; set; }

        /// <summary>
        /// reprezentacja w kodzie tabeli ID
        /// </summary>
        public int ID
        {
            get { return movies.FilmID; }
            set { movies.FilmID = value; }
        }

        /// <summary>
        /// reprezentacja w kodzie tabeli MovieType
        /// </summary>
        public string MovieType
        {
            get { return movieTypes.TypeName; }
            set { movieTypes.TypeName = value; }
        }

        /// <summary>
        /// reprezentacja w kodzie tabeli MovieName
        /// </summary>
        public string MovieName
        {
            get { return movies.MovieName; }
            set { movies.MovieName = value; }
        }

        /// <summary>
        /// reprezentacja w kodzie tabeli MovieLink
        /// </summary>
        public string Movielink
        {
            get { return movies.MovieLink; }
            set { movies.MovieLink = value; }
        }

    }
}
