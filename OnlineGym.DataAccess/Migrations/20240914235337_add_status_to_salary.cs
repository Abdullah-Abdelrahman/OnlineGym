using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_status_to_salary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Salaries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Salaries");
        }
    }
}
