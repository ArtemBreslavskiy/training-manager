using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class adddefaultvaluetoworkoutsetandplainedworkoutsetindayexercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workoutSets_dayExercises_dayExercisesId",
                table: "workoutSets");

            migrationBuilder.DropIndex(
                name: "IX_workoutSets_dayExercisesId",
                table: "workoutSets");

            migrationBuilder.AddColumn<int>(
                name: "DayExerciseId",
                table: "workoutSets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_workoutSets_DayExerciseId",
                table: "workoutSets",
                column: "DayExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_workoutSets_dayExercises_DayExerciseId",
                table: "workoutSets",
                column: "DayExerciseId",
                principalTable: "dayExercises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workoutSets_dayExercises_DayExerciseId",
                table: "workoutSets");

            migrationBuilder.DropIndex(
                name: "IX_workoutSets_DayExerciseId",
                table: "workoutSets");

            migrationBuilder.DropColumn(
                name: "DayExerciseId",
                table: "workoutSets");

            migrationBuilder.CreateIndex(
                name: "IX_workoutSets_dayExercisesId",
                table: "workoutSets",
                column: "dayExercisesId");

            migrationBuilder.AddForeignKey(
                name: "FK_workoutSets_dayExercises_dayExercisesId",
                table: "workoutSets",
                column: "dayExercisesId",
                principalTable: "dayExercises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
