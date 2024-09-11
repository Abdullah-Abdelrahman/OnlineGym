using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_StartedDate_to_TrainingPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Started",
                table: "TrainingPlans",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Started",
                table: "TrainingPlans");
        }
    }
}
