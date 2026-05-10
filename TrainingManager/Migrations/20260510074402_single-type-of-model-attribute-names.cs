using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class singletypeofmodelattributenames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "TrainingPrograms",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "TrainingPrograms",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "Exercise",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "Exercise",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "Days",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Order_index",
                table: "Days",
                newName: "OrderIndex");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "Days",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "TrainingPrograms",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "TrainingPrograms",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Exercise",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Exercise",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Days",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "OrderIndex",
                table: "Days",
                newName: "Order_index");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Days",
                newName: "Created_at");
        }
    }
}
