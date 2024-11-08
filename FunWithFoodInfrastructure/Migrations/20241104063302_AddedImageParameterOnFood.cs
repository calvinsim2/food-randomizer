using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunWithFoodInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageParameterOnFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Food",
                type: "VARBINARY(MAX)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Food");
        }
    }
}
