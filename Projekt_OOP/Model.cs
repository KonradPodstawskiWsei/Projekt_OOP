using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projekt_OOP
{
    public class Model: DbContext
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

/*
CREATE TABLE MovieTypes (
	MovieTypeID int primary key,
	TypeName varchar(50) NOT NULL
)

CREATE TABLE Movies (
	FilmID int primary key,
	MovieTypeID int foreign key references MovieTypes(MovieTypeID),
	MovieName varchar(50) NOT NULL,
	MovieLink varchar(250) NOT NULL
)

CREATE TABLE LikeMovies (
	LikeMovieID int primary key,
	MovieID int foreign key references Movies(FilmID)
)

CREATE TABLE DisLikeMovies (
	DisLikeMovieID int primary key,
	MovieID int foreign key references Movies(FilmID)
)
*/