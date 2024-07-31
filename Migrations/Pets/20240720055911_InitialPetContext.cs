using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvC_Final.Migrations.Pets
{
    /// <inheritdoc />
    public partial class InitialPetContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ownerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    petName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    petType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    breed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    age = table.Column<int>(type: "int", nullable: true),
                    info = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);

                });
            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "age", "birthday", "breed", "gender", "info", "ownerName", "petName", "petType" },
                values: new object[] { 1, null, null, "", "", "", "fill", "name", "type" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");
        }
    }
}
