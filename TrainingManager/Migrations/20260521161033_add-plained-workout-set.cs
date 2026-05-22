using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class addplainedworkoutset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "plainedRepsCount",
                table: "dayExercises");

            migrationBuilder.DropColumn(
                name: "plainedSetsCount",
                table: "dayExercises");

            migrationBuilder.DropColumn(
                name: "plainedSetsWeight",
                table: "dayExercises");

            migrationBuilder.CreateTable(
                name: "plainedWorkoutSet",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plainedRepsCount = table.Column<int>(type: "integer", nullable: true),
                    plainedSetsWeight = table.Column<double>(type: "double precision", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DayExerciseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plainedWorkoutSet", x => x.id);
                    table.ForeignKey(
                        name: "FK_plainedWorkoutSet_dayExercises_DayExerciseId",
                        column: x => x.DayExerciseId,
                        principalTable: "dayExercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_plainedWorkoutSet_DayExerciseId",
                table: "plainedWorkoutSet",
                column: "DayExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "plainedWorkoutSet");

            migrationBuilder.AddColumn<int>(
                name: "plainedRepsCount",
                table: "dayExercises",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "plainedSetsCount",
                table: "dayExercises",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "plainedSetsWeight",
                table: "dayExercises",
                type: "double precision",
                nullable: true);
        }
    }
}
