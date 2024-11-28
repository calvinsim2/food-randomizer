using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunWithFoodInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDessertModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dessert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false, defaultValueSql: "newsequentialId()"),
                    CuisineId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dessert", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dessert");
        }
    }
}
