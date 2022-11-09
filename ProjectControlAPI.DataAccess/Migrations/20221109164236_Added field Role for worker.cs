using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectControlAPI.DataAccess.Migrations
{
    public partial class AddedfieldRoleforworker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Workers");
        }
    }
}
