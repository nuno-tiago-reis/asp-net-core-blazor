using Memento.Movies.Shared.Database.Movies;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Shared.Movies
{
	/// <summary>
	/// Implements the 'MovieList' component.
	/// </summary>
	/// 
	/// <seealso cref="ComponentBase"/>
	public sealed partial class MovieList : ComponentBase
	{
		/// <summary>
		/// The movies.
		/// </summary>
		[Parameter]
		public List<Movie> Movies { get; set; }

		/// <inheritdoc />
		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(3000);

			this.Movies = new List<Movie>
			{
				new Movie
				{
					Title = "Iron Man",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 2, 2008")
				},
				new Movie
				{
					Title = "The Incredible Hulk",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("June 13, 2008")
				},
				new Movie
				{
					Title = "Iron Man 2",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 7, 2010")
				},
				new Movie
				{
					Title = "Thor",
					Genre = "Fantasy",
					ReleaseDate = DateTime.Parse("May 6, 2011")
				},
				new Movie
				{
					Title = "Captain America: The First Avenger",
					Genre = "Historic",
					ReleaseDate = DateTime.Parse("July 22, 2011")
				},
				new Movie
				{
					Title = "Marvel's The Avengers",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 4, 2012")
				},
				new Movie
				{
					Title = "Iron Man 3",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 3, 2013")
				},
				new Movie
				{
					Title = "Thor: The Dark World",
					Genre = "Fantasy",
					ReleaseDate = DateTime.Parse("November 8, 2013")
				},
				new Movie
				{
					Title = "Captain America: The Winter Soldier",
					Genre = "Historic",
					ReleaseDate = DateTime.Parse("April 4, 2014")
				},
				new Movie
				{
					Title = "Guardians of the Galaxy",
					Genre = "Sci-Fi",
					ReleaseDate = DateTime.Parse("August 1, 2014")
				},
				new Movie
				{
					Title = "Avengers: Age of Ultron",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("May 1, 2015")
				},
				new Movie
				{
					Title = "Ant-Man",
					Genre = "Heist",
					ReleaseDate = DateTime.Parse("July 17, 2015")
				},
				new Movie
				{
					Title = "Captain America: Civil War",
					Genre = "Historic",
					ReleaseDate = DateTime.Parse("May 6, 2016")
				},
				new Movie
				{
					Title = "Doctor Strange",
					Genre = "Fantasy",
					ReleaseDate = DateTime.Parse("November 4, 2016")
				},
				new Movie
				{
					Title = "Guardians of the Galaxy Vol.2",
					Genre = "Sci-Fi",
					ReleaseDate = DateTime.Parse("May 5, 2017")
				},
				new Movie
				{
					Title = "Spider-Man: Homecoming",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("July 7, 2017")
				},
				new Movie
				{
					Title = "Thor: Ragnarok",
					Genre = "Fantasy",
					ReleaseDate = DateTime.Parse("November 3, 2017")
				},
				new Movie
				{
					Title = "Black Panther",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("February 16, 2018")
				},
				new Movie
				{
					Title = "Avengers: Infinity War",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("April 27, 2018")
				},
				new Movie
				{
					Title = "Ant-Man and the Wasp",
					Genre = "Heist",
					ReleaseDate = DateTime.Parse("July 6, 2018")
				},
				new Movie
				{
					Title = "Captain Marvel",
					Genre = "Sci-Fi",
					ReleaseDate = DateTime.Parse("March 8, 2019")
				},
				new Movie
				{
					Title = "Avengers: Endgame",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("April 26, 2019")
				},
				new Movie
				{
					Title = "Spider-Man: Far From Home",
					Genre = "Action",
					ReleaseDate = DateTime.Parse("July 2, 2019")
				}
			};
		}
	}
}