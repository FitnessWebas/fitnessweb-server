using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitnessweb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Exercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "Muscles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutId",
                table: "Muscles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Equipment = table.Column<int>(type: "int", nullable: false),
                    MinutesPerSet = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    TargetDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workout_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutExercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    RepsPerSet = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Exercises_ExerciseId1",
                        column: x => x.ExerciseId1,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Workout_WorkoutId1",
                        column: x => x.WorkoutId1,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Muscles_ExerciseId",
                table: "Muscles",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Muscles_WorkoutId",
                table: "Muscles",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_UserId",
                table: "Workout",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_ExerciseId1",
                table: "WorkoutExercise",
                column: "ExerciseId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_WorkoutId1",
                table: "WorkoutExercise",
                column: "WorkoutId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Muscles_Exercises_ExerciseId",
                table: "Muscles",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Muscles_Workout_WorkoutId",
                table: "Muscles",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muscles_Exercises_ExerciseId",
                table: "Muscles");

            migrationBuilder.DropForeignKey(
                name: "FK_Muscles_Workout_WorkoutId",
                table: "Muscles");

            migrationBuilder.DropTable(
                name: "WorkoutExercise");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropIndex(
                name: "IX_Muscles_ExerciseId",
                table: "Muscles");

            migrationBuilder.DropIndex(
                name: "IX_Muscles_WorkoutId",
                table: "Muscles");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Muscles");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Muscles");
        }
    }
}
