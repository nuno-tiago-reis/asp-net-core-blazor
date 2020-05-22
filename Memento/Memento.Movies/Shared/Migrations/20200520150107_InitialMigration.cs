using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Memento.Movies.Shared.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	internal partial class InitialMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable
			(
				name: "Movies",
				columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CreatedBy = table.Column<long>(nullable: false),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedBy = table.Column<long>(nullable: true),
					UpdatedAt = table.Column<DateTime>(nullable: true),
					Title = table.Column<string>(maxLength: 250, nullable: false),
					Genre = table.Column<string>(maxLength: 50, nullable: false),
					ReleaseDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Movies", x => x.Id);
				}
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Movies_Genre",
				table: "Movies",
				column: "Genre"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Movies_ReleaseDate",
				table: "Movies",
				column: "ReleaseDate"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Movies_Title",
				table: "Movies",
				column: "Title",
				unique: true
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable
			(
				name: "Movies"
			);
		}
	}
}