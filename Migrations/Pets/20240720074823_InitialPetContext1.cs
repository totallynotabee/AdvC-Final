using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvC_Final.Migrations.Pets
{
    /// <inheritdoc />
    public partial class InitialPetContext1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "age", "birthday", "breed", "gender", "info", "ownerName", "petName", "petType" },
                values: new object[] { 1, null, null, "", "", "", "Person", "Pet", "Cat" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
