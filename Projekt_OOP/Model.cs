using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projekt_OOP
{
	public class Model : DbContext
	{
		public DbSet<MovieTypes> MovieTypes { get; set; }
		public DbSet<Movies> Movies { get; set; }
		public DbSet<LikeMovies> LikeMovies { get; set; }
		public DbSet<DisLikeMovies> DisLikeMovies { get; set; }

		public string ConnectionString { get; }
		public Model(string connectionString)
		{
			this.ConnectionString = connectionString;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(this.ConnectionString);
		}
	}

	public class MovieTypes
	{
		[Key]
		public int MovieTypeID { get; set; }

		public List<Movies> movies { get; set; } = new List<Movies>();

		public string TypeName { get; set; }
	}

	public class Movies
	{
		[Key]
		public int FilmID { get; set; }
		public int MovieTypeID { get; set; }
		public MovieTypes movieType { get; set; }
		public string MovieName { get; set; }
		public string MovieLink { get; set; }

		public List<DisLikeMovies> disLikeMovies { get; set; } = new List<DisLikeMovies>();
		public List<LikeMovies> likeMovies { get; set; } = new List<LikeMovies>();

	}


	public class LikeMovies
	{

		[Key]
		public int LikeMovieID { get; set; }
		public int MovieID { get; set; }
		public Movies movie { get; set; }

	}

	public class DisLikeMovies
	{
		[Key]
		public int DisLikeMovieID { get; set; }
		public int MovieID { get; set; }
		public Movies movie { get; set; }

	}
}