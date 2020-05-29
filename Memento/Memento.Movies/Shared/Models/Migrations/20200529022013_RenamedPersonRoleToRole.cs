using Microsoft.EntityFrameworkCore.Migrations;

namespace Memento.Movies.Shared.Models.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	internal partial class RenamedPersonRoleToRole : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn
			(
				name: "PersonRole",
				table: "MoviePersons"
			);

			migrationBuilder.AddColumn<int>
			(
				name: "Role",
				table: "MoviePersons",
				nullable: false,
				defaultValue: 0
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn
			(
				name: "Role",
				table: "MoviePersons"
			);

			migrationBuilder.AddColumn<int>
			(
				name: "PersonRole",
				table: "MoviePersons",
				type: "int",
				nullable: false,
				defaultValue: 0
			);
		}
	}
}