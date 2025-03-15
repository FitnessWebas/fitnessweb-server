using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitnessweb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_MuscleGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MuscleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MuscleGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Muscle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MuscleGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Muscle_MuscleGroups_MuscleGroupId",
                        column: x => x.MuscleGroupId,
                        principalTable: "MuscleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Muscle_MuscleGroupId",
                table: "Muscle",
                column: "MuscleGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Muscle");

            migrationBuilder.DropTable(
                name: "MuscleGroups");
        }
    }
}
