using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_ClientSubscriptionId_to_TrainingPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientSubscriptionId",
                table: "TrainingPlans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_ClientSubscriptionId",
                table: "TrainingPlans",
                column: "ClientSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_ClientSubscriptions_ClientSubscriptionId",
                table: "TrainingPlans",
                column: "ClientSubscriptionId",
                principalTable: "ClientSubscriptions",
                principalColumn: "ClientSubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_ClientSubscriptions_ClientSubscriptionId",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_ClientSubscriptionId",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "ClientSubscriptionId",
                table: "TrainingPlans");
        }
    }
}
