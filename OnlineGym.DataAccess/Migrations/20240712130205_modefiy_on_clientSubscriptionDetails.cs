using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modefiy_on_clientSubscriptionDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "ClientSubscriptions");

            migrationBuilder.DropColumn(
                name: "ClientPhone",
                table: "ClientSubscriptions");

            migrationBuilder.DropColumn(
                name: "end_date",
                table: "ClientSubscriptions");

            migrationBuilder.DropColumn(
                name: "paymentDate",
                table: "ClientSubscriptions");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "ClientSubscriptions");

            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "ClientSubscriptionDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientPhone",
                table: "ClientSubscriptionDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "ClientSubscriptionDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "ClientSubscriptionDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "paymentDate",
                table: "ClientSubscriptionDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "ClientPhone",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ClientSubscriptionDetails");

            migrationBuilder.DropColumn(
                name: "paymentDate",
                table: "ClientSubscriptionDetails");

            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "ClientSubscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientPhone",
                table: "ClientSubscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "end_date",
                table: "ClientSubscriptions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "paymentDate",
                table: "ClientSubscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateOnly>(
                name: "start_date",
                table: "ClientSubscriptions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
