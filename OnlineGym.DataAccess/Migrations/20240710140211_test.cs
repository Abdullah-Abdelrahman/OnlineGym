using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefit_Subscriptions_SubscriptionId",
                table: "Benefit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Benefit",
                table: "Benefit");

            migrationBuilder.RenameTable(
                name: "Benefit",
                newName: "Benefits");

            migrationBuilder.RenameIndex(
                name: "IX_Benefit_SubscriptionId",
                table: "Benefits",
                newName: "IX_Benefits_SubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Benefits",
                table: "Benefits",
                column: "BenefitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_Subscriptions_SubscriptionId",
                table: "Benefits",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "subscription_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_Subscriptions_SubscriptionId",
                table: "Benefits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Benefits",
                table: "Benefits");

            migrationBuilder.RenameTable(
                name: "Benefits",
                newName: "Benefit");

            migrationBuilder.RenameIndex(
                name: "IX_Benefits_SubscriptionId",
                table: "Benefit",
                newName: "IX_Benefit_SubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Benefit",
                table: "Benefit",
                column: "BenefitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefit_Subscriptions_SubscriptionId",
                table: "Benefit",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "subscription_id");
        }
    }
}
