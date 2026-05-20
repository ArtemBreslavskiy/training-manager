using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class addnotestoexercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "exercises",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notes",
                table: "exercises");
        }
    }
}
