using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManager.Migrations
{
    /// <inheritdoc />
    public partial class addorderinexerciseinplainedworkoutset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "orderInExercises",
                table: "workoutSets",
                newName: "orderInExercise");

            migrationBuilder.AddColumn<int>(
                name: "orderInExercise",
                table: "plainedWorkoutSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderInExercise",
                table: "plainedWorkoutSet");

            migrationBuilder.RenameColumn(
                name: "orderInExercise",
                table: "workoutSets",
                newName: "orderInExercises");
        }
    }
}
