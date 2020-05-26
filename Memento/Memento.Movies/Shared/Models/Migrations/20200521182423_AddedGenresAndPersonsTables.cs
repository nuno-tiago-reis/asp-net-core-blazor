using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Memento.Movies.Shared.Models.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	internal partial class AddedGenresAndPersonsTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex
			(
				name: "IX_Movies_Genre",
				table: "Movies"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Movies_Title",
				table: "Movies"
			);

			migrationBuilder.DropColumn
			(
				name: "Genre",
				table: "Movies"
			);

			migrationBuilder.DropColumn
			(
				name: "Title",
				table: "Movies"
			);

			migrationBuilder.AddColumn<bool>
			(
				name: "InTheaters",
				table: "Movies",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<string>
			(
				name: "Name",
				table: "Movies",
				maxLength: 250,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				name: "NormalizedName",
				table: "Movies",
				maxLength: 250,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				name: "PictureUrl",
				table: "Movies",
				maxLength: 250,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				name: "Summary",
				table: "Movies",
				maxLength: 500,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				name: "TrailerUrl",
				table: "Movies",
				maxLength: 250,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.CreateTable
			(
				name: "Genres",
				columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CreatedBy = table.Column<long>(nullable: false),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedBy = table.Column<long>(nullable: true),
					UpdatedAt = table.Column<DateTime>(nullable: true),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					NormalizedName = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Genres", x => x.Id);
				}
			);

			migrationBuilder.CreateTable
			(
				name: "Persons",
				columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CreatedBy = table.Column<long>(nullable: false),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedBy = table.Column<long>(nullable: true),
					UpdatedAt = table.Column<DateTime>(nullable: true),
					Name = table.Column<string>(maxLength: 250, nullable: false),
					NormalizedName = table.Column<string>(maxLength: 250, nullable: false),
					Biography = table.Column<string>(maxLength: 500, nullable: false),
					PictureUrl = table.Column<string>(maxLength: 250, nullable: false),
					BirthDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Persons", x => x.Id);
				}
			);

			migrationBuilder.CreateTable
			(
				name: "MovieGenres",
				columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CreatedBy = table.Column<long>(nullable: false),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedBy = table.Column<long>(nullable: true),
					UpdatedAt = table.Column<DateTime>(nullable: true),
					MovieId = table.Column<long>(nullable: false),
					GenreId = table.Column<long>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MovieGenres", x => x.Id);
					table.ForeignKey
					(
						name: "FK_MovieGenres_Genres_GenreId",
						column: x => x.GenreId,
						principalTable: "Genres",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
					table.ForeignKey
					(
						name: "FK_MovieGenres_Movies_MovieId",
						column: x => x.MovieId,
						principalTable: "Movies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				name: "MoviePersons",
				columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CreatedBy = table.Column<long>(nullable: false),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedBy = table.Column<long>(nullable: true),
					UpdatedAt = table.Column<DateTime>(nullable: true),
					MovieId = table.Column<long>(nullable: false),
					PersonId = table.Column<long>(nullable: false),
					PersonRole = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MoviePersons", x => x.Id);
					table.ForeignKey
					(
						name: "FK_MoviePersons_Movies_MovieId",
						column: x => x.MovieId,
						principalTable: "Movies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
					table.ForeignKey
					(
						name: "FK_MoviePersons_Persons_PersonId",
						column: x => x.PersonId,
						principalTable: "Persons",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Movies_Name",
				table: "Movies",
				column: "Name"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Movies_NormalizedName",
				table: "Movies",
				column: "NormalizedName"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Movies_NormalizedName_ReleaseDate",
				table: "Movies",
				columns: new[] { "NormalizedName", "ReleaseDate" },
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Genres_Name",
				table: "Genres",
				column: "Name",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Genres_NormalizedName",
				table: "Genres",
				column: "NormalizedName",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_MovieGenres_GenreId",
				table: "MovieGenres",
				column: "GenreId"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_MovieGenres_MovieId",
				table: "MovieGenres",
				column: "MovieId"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_MoviePersons_MovieId",
				table: "MoviePersons",
				column: "MovieId"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_MoviePersons_PersonId",
				table: "MoviePersons",
				column: "PersonId"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Persons_BirthDate",
				table: "Persons",
				column: "BirthDate"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Persons_Name",
				table: "Persons",
				column: "Name"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Persons_NormalizedName",
				table: "Persons",
				column: "NormalizedName"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Persons_NormalizedName_BirthDate",
				table: "Persons",
				columns: new[] { "NormalizedName", "BirthDate" },
				unique: true
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable
			(
				name: "MovieGenres"
			);

			migrationBuilder.DropTable
			(
				name: "MoviePersons"
			);

			migrationBuilder.DropTable
			(
				name: "Genres"
			);

			migrationBuilder.DropTable
			(
				name: "Persons"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Movies_Name",
				table: "Movies"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Movies_NormalizedName",
				table: "Movies"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Movies_NormalizedName_ReleaseDate",
				table: "Movies"
			);

			migrationBuilder.DropColumn
			(
				name: "InTheaters",
				table: "Movies"
			);

			migrationBuilder.DropColumn
			(
				name: "Name",
				table: "Movies"
			);

			migrationBuilder.DropColumn
			(
				name: "NormalizedName",
				table: "Movies"
			);

			migrationBuilder.DropColumn
			(
				name: "PictureUrl",
				table: "Movies"
			);

			migrationBuilder.DropColumn
			(
				name: "Summary",
				table: "Movies"
			);

			migrationBuilder.DropColumn
			(
				name: "TrailerUrl",
				table: "Movies"
			);

			migrationBuilder.AddColumn<string>
			(
				name: "Genre",
				table: "Movies",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				name: "Title",
				table: "Movies",
				type: "nvarchar(250)",
				maxLength: 250,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Movies_Genre",
				table: "Movies",
				column: "Genre"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Movies_Title",
				table: "Movies",
				column: "Title",
				unique: true
			);
		}
	}
}