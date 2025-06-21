using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB14.Migrations
{
    /// <inheritdoc />
    public partial class eliminaciónlogica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Grados",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cursos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Grados");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cursos");
        }
    }
}
