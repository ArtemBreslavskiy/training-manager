using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class FixTablesAndColumnsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayExercises_Days_DayId",
                table: "DayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_DayExercises_Exercise_ExercisesId",
                table: "DayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Days_TrainingPrograms_TrainingProgramId",
                table: "Days");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSession_Days_DayId",
                table: "WorkoutSession");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSet_DayExercises_DayExercisesId",
                table: "WorkoutSet");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSet_WorkoutSession_WorkoutSessionId",
                table: "WorkoutSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPrograms",
                table: "TrainingPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Days",
                table: "Days");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayExercises",
                table: "DayExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSet",
                table: "WorkoutSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSession",
                table: "WorkoutSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.RenameTable(
                name: "TrainingPrograms",
                newName: "trainingPrograms");

            migrationBuilder.RenameTable(
                name: "Days",
                newName: "days");

            migrationBuilder.RenameTable(
                name: "DayExercises",
                newName: "dayExercises");

            migrationBuilder.RenameTable(
                name: "WorkoutSet",
                newName: "workoutSets");

            migrationBuilder.RenameTable(
                name: "WorkoutSession",
                newName: "workoutSessions");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "exercises");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "trainingPrograms",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "trainingPrograms",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "trainingPrograms",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "trainingPrograms",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Days",
                table: "trainingPrograms",
                newName: "daysCount");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "days",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "OrderIndex",
                table: "days",
                newName: "orderIndex");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "days",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "days",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "days",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Days_TrainingProgramId",
                table: "days",
                newName: "IX_days_TrainingProgramId");

            migrationBuilder.RenameColumn(
                name: "OrderInDay",
                table: "dayExercises",
                newName: "orderInDay");

            migrationBuilder.RenameColumn(
                name: "DayId",
                table: "dayExercises",
                newName: "dayId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "dayExercises",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PlainedWeight",
                table: "dayExercises",
                newName: "plainedSetsWeight");

            migrationBuilder.RenameColumn(
                name: "PlainedSets",
                table: "dayExercises",
                newName: "plainedSetsCount");

            migrationBuilder.RenameColumn(
                name: "PlainedReps",
                table: "dayExercises",
                newName: "plainedRepsCount");

            migrationBuilder.RenameColumn(
                name: "ExercisesId",
                table: "dayExercises",
                newName: "exerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_DayExercises_DayId",
                table: "dayExercises",
                newName: "IX_dayExercises_dayId");

            migrationBuilder.RenameIndex(
                name: "IX_DayExercises_ExercisesId",
                table: "dayExercises",
                newName: "IX_dayExercises_exerciseId");

            migrationBuilder.RenameColumn(
                name: "OrderInExercises",
                table: "workoutSets",
                newName: "orderInExercises");

            migrationBuilder.RenameColumn(
                name: "DayExercisesId",
                table: "workoutSets",
                newName: "dayExercisesId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "workoutSets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "workoutSets",
                newName: "weigth");

            migrationBuilder.RenameColumn(
                name: "IsComplited",
                table: "workoutSets",
                newName: "isCompleted");

            migrationBuilder.RenameColumn(
                name: "Reps",
                table: "workoutSets",
                newName: "repsCount");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSet_WorkoutSessionId",
                table: "workoutSets",
                newName: "IX_workoutSets_WorkoutSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSet_DayExercisesId",
                table: "workoutSets",
                newName: "IX_workoutSets_dayExercisesId");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "workoutSessions",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "DayId",
                table: "workoutSessions",
                newName: "dayId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "workoutSessions",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "workoutSessions",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSession_DayId",
                table: "workoutSessions",
                newName: "IX_workoutSessions_dayId");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "exercises",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "exercises",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "exercises",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "exercises",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainingPrograms",
                table: "trainingPrograms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_days",
                table: "days",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dayExercises",
                table: "dayExercises",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workoutSets",
                table: "workoutSets",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workoutSessions",
                table: "workoutSessions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_exercises",
                table: "exercises",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dayExercises_days_dayId",
                table: "dayExercises",
                column: "dayId",
                principalTable: "days",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dayExercises_exercises_exerciseId",
                table: "dayExercises",
                column: "exerciseId",
                principalTable: "exercises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_days_trainingPrograms_TrainingProgramId",
                table: "days",
                column: "TrainingProgramId",
                principalTable: "trainingPrograms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workoutSessions_days_dayId",
                table: "workoutSessions",
                column: "dayId",
                principalTable: "days",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workoutSets_dayExercises_dayExercisesId",
                table: "workoutSets",
                column: "dayExercisesId",
                principalTable: "dayExercises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workoutSets_workoutSessions_WorkoutSessionId",
                table: "workoutSets",
                column: "WorkoutSessionId",
                principalTable: "workoutSessions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dayExercises_days_dayId",
                table: "dayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_dayExercises_exercises_exerciseId",
                table: "dayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_days_trainingPrograms_TrainingProgramId",
                table: "days");

            migrationBuilder.DropForeignKey(
                name: "FK_workoutSessions_days_dayId",
                table: "workoutSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_workoutSets_dayExercises_dayExercisesId",
                table: "workoutSets");

            migrationBuilder.DropForeignKey(
                name: "FK_workoutSets_workoutSessions_WorkoutSessionId",
                table: "workoutSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainingPrograms",
                table: "trainingPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_days",
                table: "days");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dayExercises",
                table: "dayExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workoutSets",
                table: "workoutSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workoutSessions",
                table: "workoutSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_exercises",
                table: "exercises");

            migrationBuilder.RenameTable(
                name: "trainingPrograms",
                newName: "TrainingPrograms");

            migrationBuilder.RenameTable(
                name: "days",
                newName: "Days");

            migrationBuilder.RenameTable(
                name: "dayExercises",
                newName: "DayExercises");

            migrationBuilder.RenameTable(
                name: "workoutSets",
                newName: "WorkoutSet");

            migrationBuilder.RenameTable(
                name: "workoutSessions",
                newName: "WorkoutSession");

            migrationBuilder.RenameTable(
                name: "exercises",
                newName: "Exercise");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "TrainingPrograms",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "TrainingPrograms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "TrainingPrograms",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TrainingPrograms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "daysCount",
                table: "TrainingPrograms",
                newName: "Days");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Days",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "orderIndex",
                table: "Days",
                newName: "OrderIndex");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Days",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Days",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Days",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_days_TrainingProgramId",
                table: "Days",
                newName: "IX_Days_TrainingProgramId");

            migrationBuilder.RenameColumn(
                name: "orderInDay",
                table: "DayExercises",
                newName: "OrderInDay");

            migrationBuilder.RenameColumn(
                name: "dayId",
                table: "DayExercises",
                newName: "DayId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DayExercises",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "plainedSetsWeight",
                table: "DayExercises",
                newName: "PlainedWeight");

            migrationBuilder.RenameColumn(
                name: "plainedSetsCount",
                table: "DayExercises",
                newName: "PlainedSets");

            migrationBuilder.RenameColumn(
                name: "plainedRepsCount",
                table: "DayExercises",
                newName: "PlainedReps");

            migrationBuilder.RenameColumn(
                name: "exerciseId",
                table: "DayExercises",
                newName: "ExercisesId");

            migrationBuilder.RenameIndex(
                name: "IX_dayExercises_dayId",
                table: "DayExercises",
                newName: "IX_DayExercises_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_dayExercises_exerciseId",
                table: "DayExercises",
                newName: "IX_DayExercises_ExercisesId");

            migrationBuilder.RenameColumn(
                name: "orderInExercises",
                table: "WorkoutSet",
                newName: "OrderInExercises");

            migrationBuilder.RenameColumn(
                name: "dayExercisesId",
                table: "WorkoutSet",
                newName: "DayExercisesId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "WorkoutSet",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "weigth",
                table: "WorkoutSet",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "isCompleted",
                table: "WorkoutSet",
                newName: "IsComplited");

            migrationBuilder.RenameColumn(
                name: "repsCount",
                table: "WorkoutSet",
                newName: "Reps");

            migrationBuilder.RenameIndex(
                name: "IX_workoutSets_WorkoutSessionId",
                table: "WorkoutSet",
                newName: "IX_WorkoutSet_WorkoutSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_workoutSets_dayExercisesId",
                table: "WorkoutSet",
                newName: "IX_WorkoutSet_DayExercisesId");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "WorkoutSession",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "dayId",
                table: "WorkoutSession",
                newName: "DayId");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "WorkoutSession",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "WorkoutSession",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_workoutSessions_dayId",
                table: "WorkoutSession",
                newName: "IX_WorkoutSession_DayId");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Exercise",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Exercise",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Exercise",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Exercise",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPrograms",
                table: "TrainingPrograms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Days",
                table: "Days",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayExercises",
                table: "DayExercises",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSet",
                table: "WorkoutSet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSession",
                table: "WorkoutSession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DayExercises_Days_DayId",
                table: "DayExercises",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DayExercises_Exercise_ExercisesId",
                table: "DayExercises",
                column: "ExercisesId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Days_TrainingPrograms_TrainingProgramId",
                table: "Days",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSession_Days_DayId",
                table: "WorkoutSession",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSet_DayExercises_DayExercisesId",
                table: "WorkoutSet",
                column: "DayExercisesId",
                principalTable: "DayExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSet_WorkoutSession_WorkoutSessionId",
                table: "WorkoutSet",
                column: "WorkoutSessionId",
                principalTable: "WorkoutSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
