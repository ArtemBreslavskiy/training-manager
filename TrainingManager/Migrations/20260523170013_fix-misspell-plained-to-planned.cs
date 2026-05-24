using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class fixmisspellplainedtoplanned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "plainedWorkoutSet");

            migrationBuilder.AlterColumn<int>(
                name: "repsCount",
                table: "workoutSets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "plannedWorkoutSet",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderInExercise = table.Column<int>(type: "integer", nullable: false),
                    plannedRepsCount = table.Column<int>(type: "integer", nullable: true),
                    plannedSetsWeight = table.Column<double>(type: "double precision", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DayExerciseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plannedWorkoutSet", x => x.id);
                    table.ForeignKey(
                        name: "FK_plannedWorkoutSet_dayExercises_DayExerciseId",
                        column: x => x.DayExerciseId,
                        principalTable: "dayExercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_plannedWorkoutSet_DayExerciseId",
                table: "plannedWorkoutSet",
                column: "DayExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "plannedWorkoutSet");

            migrationBuilder.AlterColumn<int>(
                name: "repsCount",
                table: "workoutSets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "plainedWorkoutSet",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DayExerciseId = table.Column<int>(type: "integer", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    orderInExercise = table.Column<int>(type: "integer", nullable: false),
                    plainedRepsCount = table.Column<int>(type: "integer", nullable: true),
                    plainedSetsWeight = table.Column<double>(type: "double precision", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
    }
}
