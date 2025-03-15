using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitnessweb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Muscles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muscle_MuscleGroups_MuscleGroupId",
                table: "Muscle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Muscle",
                table: "Muscle");

            migrationBuilder.RenameTable(
                name: "Muscle",
                newName: "Muscles");

            migrationBuilder.RenameIndex(
                name: "IX_Muscle_MuscleGroupId",
                table: "Muscles",
                newName: "IX_Muscles_MuscleGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Muscles",
                table: "Muscles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Muscles_MuscleGroups_MuscleGroupId",
                table: "Muscles",
                column: "MuscleGroupId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muscles_MuscleGroups_MuscleGroupId",
                table: "Muscles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Muscles",
                table: "Muscles");

            migrationBuilder.RenameTable(
                name: "Muscles",
                newName: "Muscle");

            migrationBuilder.RenameIndex(
                name: "IX_Muscles_MuscleGroupId",
                table: "Muscle",
                newName: "IX_Muscle_MuscleGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Muscle",
                table: "Muscle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Muscle_MuscleGroups_MuscleGroupId",
                table: "Muscle",
                column: "MuscleGroupId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
