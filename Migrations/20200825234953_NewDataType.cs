using Microsoft.EntityFrameworkCore.Migrations;

namespace BartenderApp.Migrations
{
    public partial class NewDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PickUpDrink",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickUpDrink",
                table: "Orders");
        }
    }
}
