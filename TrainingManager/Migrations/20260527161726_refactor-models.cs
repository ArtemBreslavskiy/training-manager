using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class refactormodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plannedWorkoutSet_dayExercises_DayExerciseId",
                table: "plannedWorkoutSet");

            migrationBuilder.DropForeignKey(
                name: "FK_workoutSets_dayExercises_dayExercisesId",
                table: "workoutSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plannedWorkoutSet",
                table: "plannedWorkoutSet");

            migrationBuilder.RenameTable(
                name: "plannedWorkoutSet",
                newName: "plannedWorkoutSets");

            migrationBuilder.RenameColumn(
                name: "dayExercisesId",
                table: "workoutSets",
                newName: "dayExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_workoutSets_dayExercisesId",
                table: "workoutSets",
                newName: "IX_workoutSets_dayExerciseId");

            migrationBuilder.RenameColumn(
                name: "DayExerciseId",
                table: "plannedWorkoutSets",
                newName: "dayExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_plannedWorkoutSet_DayExerciseId",
                table: "plannedWorkoutSets",
                newName: "IX_plannedWorkoutSets_dayExerciseId");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "trainingPrograms",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "days",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_plannedWorkoutSets",
                table: "plannedWorkoutSets",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_plannedWorkoutSets_dayExercises_dayExerciseId",
                table: "plannedWorkoutSets",
                column: "dayExerciseId",
                principalTable: "dayExercises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workoutSets_dayExercises_dayExerciseId",
                table: "workoutSets",
                column: "dayExerciseId",
                principalTable: "dayExercises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plannedWorkoutSets_dayExercises_dayExerciseId",
                table: "plannedWorkoutSets");

            migrationBuilder.DropForeignKey(
                name: "FK_workoutSets_dayExercises_dayExerciseId",
                table: "workoutSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plannedWorkoutSets",
                table: "plannedWorkoutSets");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "trainingPrograms");

            migrationBuilder.RenameTable(
                name: "plannedWorkoutSets",
                newName: "plannedWorkoutSet");

            migrationBuilder.RenameColumn(
                name: "dayExerciseId",
                table: "workoutSets",
                newName: "dayExercisesId");

            migrationBuilder.RenameIndex(
                name: "IX_workoutSets_dayExerciseId",
                table: "workoutSets",
                newName: "IX_workoutSets_dayExercisesId");

            migrationBuilder.RenameColumn(
                name: "dayExerciseId",
                table: "plannedWorkoutSet",
                newName: "DayExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_plannedWorkoutSets_dayExerciseId",
                table: "plannedWorkoutSet",
                newName: "IX_plannedWorkoutSet_DayExerciseId");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "days",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_plannedWorkoutSet",
                table: "plannedWorkoutSet",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_plannedWorkoutSet_dayExercises_DayExerciseId",
                table: "plannedWorkoutSet",
                column: "DayExerciseId",
                principalTable: "dayExercises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
