using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    public partial class RemoveKeyFromAttributeAndProductType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Attribute");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
