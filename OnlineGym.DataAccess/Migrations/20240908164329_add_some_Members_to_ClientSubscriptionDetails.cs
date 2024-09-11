using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_some_Members_to_ClientSubscriptionDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "ClientSubscriptionDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "BodyFat",
                table: "ClientSubscriptionDetails",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diseases",
                table: "ClientSubscriptionDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "ClientSubscriptionDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hight",
                table: "ClientSubscriptionDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "ClientSubscriptionDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "ClientSubscriptionDetails",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "BodyFat",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "Diseases",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "Hight",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ClientSubscriptionDetails");
        }
    }
}
