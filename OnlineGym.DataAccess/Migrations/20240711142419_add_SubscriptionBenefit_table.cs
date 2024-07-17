using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_SubscriptionBenefit_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_Subscriptions_SubscriptionId",
                table: "Benefits");

            migrationBuilder.DropIndex(
                name: "IX_Benefits_SubscriptionId",
                table: "Benefits");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Benefits");

            migrationBuilder.CreateTable(
                name: "SubscriptionBenefits",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    BenefitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionBenefits", x => new { x.BenefitId, x.SubscriptionId });
                    table.ForeignKey(
                        name: "FK_SubscriptionBenefits_Benefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "Benefits",
                        principalColumn: "BenefitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionBenefits_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "subscription_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionBenefits_SubscriptionId",
                table: "SubscriptionBenefits",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionBenefits");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Benefits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Benefits_SubscriptionId",
                table: "Benefits",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_Subscriptions_SubscriptionId",
                table: "Benefits",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "subscription_id");
        }
    }
}
