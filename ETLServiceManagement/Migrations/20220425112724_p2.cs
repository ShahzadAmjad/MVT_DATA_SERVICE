using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETLServiceManagement.Migrations
{
    public partial class p2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DbName",
                table: "services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DbUrl",
                table: "services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DestinationDb",
                table: "services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceFolder",
                table: "services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DbName",
                table: "services");

            migrationBuilder.DropColumn(
                name: "DbUrl",
                table: "services");

            migrationBuilder.DropColumn(
                name: "DestinationDb",
                table: "services");

            migrationBuilder.DropColumn(
                name: "SourceFolder",
                table: "services");

            migrationBuilder.DropColumn(
                name: "TableName",
                table: "services");
        }
    }
}
