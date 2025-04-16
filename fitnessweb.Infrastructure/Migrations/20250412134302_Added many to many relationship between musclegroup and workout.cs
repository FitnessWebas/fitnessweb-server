using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitnessweb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedmanytomanyrelationshipbetweenmusclegroupandworkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuscleGroups_Workouts_WorkoutId",
                table: "MuscleGroups");

            migrationBuilder.DropIndex(
                name: "IX_MuscleGroups_WorkoutId",
                table: "MuscleGroups");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "MuscleGroups");

            migrationBuilder.CreateTable(
                name: "MuscleGroupWorkout",
                columns: table => new
                {
                    MuscleGroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroupWorkout", x => new { x.MuscleGroupsId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_MuscleGroupWorkout_MuscleGroups_MuscleGroupsId",
                        column: x => x.MuscleGroupsId,
                        principalTable: "MuscleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuscleGroupWorkout_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MuscleGroupWorkout_WorkoutsId",
                table: "MuscleGroupWorkout",
                column: "WorkoutsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MuscleGroupWorkout");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutId",
                table: "MuscleGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MuscleGroups_WorkoutId",
                table: "MuscleGroups",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleGroups_Workouts_WorkoutId",
                table: "MuscleGroups",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }
    }
}
