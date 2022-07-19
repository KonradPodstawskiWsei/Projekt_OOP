using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;

namespace Projekt_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int actualMovieNumber = 0;
        public string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Projekt_OOP;Integrated Security=True";
        public List<Movie> movies = new List<Movie>();
        public List<string> GlobalmoviesLinks = new List<string>();
        public bool is_player = false;

        public MainWindow()
        {
            InitializeComponent();

            List<LikeMovies> likeMovies;
            List<string> LikeMoviesName = new List<string>();

            List<DisLikeMovies> nonLikeMovies;
            List<string> nonLikeMoviesName = new List<string>();

            List<Movies> movies;
            List<string> moviesTitle = new List<string>();

            using (Model db = new Model(connectionString))
            {
                movies = db.Movies.ToList();
                likeMovies = db.LikeMovies.ToList();
                nonLikeMovies = db.DisLikeMovies.ToList();
            }

            foreach (Movies movie in movies)
            {
                moviesTitle.Add(movie.MovieName);
            }

            foreach (Movies movie in movies)
            {
                foreach(LikeMovies likeMovie in likeMovies)
                {
                    if ( likeMovie.LikeMovieID == movie.FilmID )
                    {
                        LikeMoviesName.Add(movie.MovieName);
                    }
                }
            }

            foreach (Movies movie in movies)
            {
                foreach (DisLikeMovies nonLikeMovie in nonLikeMovies)
                {
                    if (nonLikeMovie.DisLikeMovieID == movie.FilmID)
                    {
                        nonLikeMoviesName.Add(movie.MovieName);
                    }
                }
            }

            approve.ItemsSource = LikeMoviesName;
            Nonapprove.ItemsSource = nonLikeMoviesName;
            allList.ItemsSource = moviesTitle;
        }

        private void films_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Browser.Visibility = Visibility.Visible;
            this.checker.Visibility = Visibility.Hidden;

            List<Movies> movies;
            using (Model db = new Model(connectionString))
            {
                movies = db.Movies.ToList();
            }

            List<string> moviesLinks = new List<string>();
            List<string> moviesTtiles = new List<string>();
            foreach (Movies movie in movies)
            {
                moviesLinks.Add(movie.MovieLink);
                moviesTtiles.Add(movie.MovieName);
            }

            VideoTitleComponent.Content = moviesTtiles[actualMovieNumber];

            string path = Directory.GetCurrentDirectory();
            string to = path + "\\youtube_player.html";

            string link = moviesLinks[actualMovieNumber];

            string player_template = @"<style>
                                        * {
                                            margin: 0;
                                            padding: 0;
                                            border: 0;
                                        }</style>
            <iframe width='100%' height='100%' src='{link}' title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard - write; encrypted - media; gyroscope; picture -in-picture' allowfullscreen></iframe>";

            string text = player_template;
            text = text.Replace("{link}", link);
            File.WriteAllText(to, text);

            this.Browser.Address = to;

        }

        private void like_Click(object sender, RoutedEventArgs e)
        {

            List<Movies> movies;
            List<string> moviesTitle = new List<string>();

            List<LikeMovies> likeMovies = new List<LikeMovies>();
            List<string> LikeMoviesName = new List<string>();

            using (Model db = new Model(connectionString))
            {
                movies = db.Movies.ToList();
                LikeMovies entity = new LikeMovies() { LikeMovieID = movies[actualMovieNumber].FilmID, MovieID = movies[actualMovieNumber].FilmID };
                db.LikeMovies.Add(entity);
                db.SaveChanges();

                likeMovies = db.LikeMovies.ToList();
            }

            foreach (Movies movie in movies)
            {
                foreach (LikeMovies likeMovie in likeMovies)
                {
                    if (likeMovie.LikeMovieID == movie.FilmID)
                    {
                        LikeMoviesName.Add(movie.MovieName);
                    }
                }
            }

            approve.ItemsSource = LikeMoviesName;
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (!is_player)
            {
                List<Movies> movies;
                using (Model db = new Model(connectionString))
                {
                    movies = db.Movies.ToList();
                }

                if (actualMovieNumber == movies.Count - 1)
                {
                    this.actualMovieNumber = 0;
                }
                else
                {
                    this.actualMovieNumber += 1;
                }

                List<string> moviesLinks = new List<string>();
                List<string> moviesTtiles = new List<string>();
                foreach (Movies movie in movies)
                {
                    moviesLinks.Add(movie.MovieLink);
                    moviesTtiles.Add(movie.MovieName);
                }

                VideoTitleComponent.Content = moviesTtiles[actualMovieNumber];

                string path = Directory.GetCurrentDirectory();
                string to = path + "\\youtube_player.html";

                string link = moviesLinks[actualMovieNumber];

                string player_template = @"<style>
                                        * {
                                            margin: 0;
                                            padding: 0;
                                            border: 0;
                                        }</style>
            <iframe width='100%' height='100%' src='{link}' title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard - write; encrypted - media; gyroscope; picture -in-picture' allowfullscreen></iframe>";

                string text = player_template;
                text = text.Replace("{link}", link);
                File.WriteAllText(to, text);

                this.Browser.Address = to;
            } else
            {


                List<string> moviesLinks = new List<string>();
                List<string> moviesTtiles = new List<string>();

                if (actualMovieNumber == GlobalmoviesLinks.Count - 1)
                {
                    this.actualMovieNumber = 0;
                }
                else
                {
                    this.actualMovieNumber += 1;
                }

                foreach (string movie in GlobalmoviesLinks)
                {
                    moviesLinks.Add(movie);
                    moviesTtiles.Add("");
                }

                VideoTitleComponent.Content = "";

                string path = Directory.GetCurrentDirectory();
                string to = path + "\\youtube_player.html";

                string link = GlobalmoviesLinks[actualMovieNumber];

                string player_template = @"<style>
                                        * {
                                            margin: 0;
                                            padding: 0;
                                            border: 0;
                                        }</style>
            <iframe width='100%' height='100%' src='{link}' title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard - write; encrypted - media; gyroscope; picture -in-picture' allowfullscreen></iframe>";

                string text = player_template;
                text = text.Replace("{link}", link);
                File.WriteAllText(to, text);

                this.Browser.Address = to;
            }
        }

        private void dislike_Click(object sender, RoutedEventArgs e)
        {
            List<Movies> movies;
            List<string> moviesTitle = new List<string>();

            List<DisLikeMovies> dislikeMovies = new List<DisLikeMovies>();
            List<string> disLikeMoviesName = new List<string>();

            using (Model db = new Model(connectionString))
            {
                movies = db.Movies.ToList();
                DisLikeMovies entity = new DisLikeMovies() { DisLikeMovieID = movies[actualMovieNumber].FilmID, MovieID = movies[actualMovieNumber].FilmID };
                db.DisLikeMovies.Add(entity);
                db.SaveChanges();

                dislikeMovies = db.DisLikeMovies.ToList();
            }

            foreach (Movies movie in movies)
            {
                foreach (DisLikeMovies dislikeMovie in dislikeMovies)
                {
                    if (dislikeMovie.DisLikeMovieID == movie.FilmID)
                    {
                        disLikeMoviesName.Add(movie.MovieName);

                    }
                }
            }

            Nonapprove.ItemsSource = disLikeMoviesName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            actualMovieNumber = 0;
            approve.Visibility = Visibility.Hidden;
            Nonapprove.Visibility = Visibility.Hidden;
            allList.Visibility = Visibility.Hidden;

            aproveName.Visibility = Visibility.Hidden;
            nonAproveName.Visibility = Visibility.Hidden;
            allListName.Visibility = Visibility.Hidden;
            like.Visibility = Visibility.Hidden;
            dislike.Visibility = Visibility.Hidden;

            List<Movies> movies = new List<Movies>();
            List<string> moviesTitle = new List<string>();
           // List<string> moviesLinks = new List<string>();

            List<LikeMovies> likeMovies = new List<LikeMovies>();
            List<string> disLikeMoviesName = new List<string>();

            using (Model db = new Model(connectionString))
            {
                movies = db.Movies.ToList();

                likeMovies = db.LikeMovies.ToList();
            }

            foreach (LikeMovies likeMovie in likeMovies)
            {
                GlobalmoviesLinks.Add(likeMovie.movie.MovieLink.ToString());
            }

            is_player = true;

        }
    }
}
