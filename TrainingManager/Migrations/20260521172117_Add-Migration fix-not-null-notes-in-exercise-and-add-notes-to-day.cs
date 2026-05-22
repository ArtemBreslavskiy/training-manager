using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationfixnotnullnotesinexerciseandaddnotestoday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "orderIndex",
                table: "days",
                newName: "orderInProgram");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "exercises",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "days",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notes",
                table: "days");

            migrationBuilder.RenameColumn(
                name: "orderInProgram",
                table: "days",
                newName: "orderIndex");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "exercises",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
