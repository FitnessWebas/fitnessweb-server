using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitnessweb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatedworkoutentitytousemusclegroupscollectioninsteadofmuscles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muscles_Workouts_WorkoutId",
                table: "Muscles");

            migrationBuilder.DropIndex(
                name: "IX_Muscles_WorkoutId",
                table: "Muscles");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Muscles");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutId",
                table: "Muscles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Muscles_WorkoutId",
                table: "Muscles",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Muscles_Workouts_WorkoutId",
                table: "Muscles",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }
    }
}
