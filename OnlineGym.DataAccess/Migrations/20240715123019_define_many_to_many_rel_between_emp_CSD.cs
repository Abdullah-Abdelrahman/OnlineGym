using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class define_many_to_many_rel_between_emp_CSD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ClientSubscriptionDetailsEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientSubscriptionDetailsEmployees",
                table: "ClientSubscriptionDetailsEmployees");

            migrationBuilder.DropIndex(
                name: "IX_ClientSubscriptionDetailsEmployees_EmployeeId",
                table: "ClientSubscriptionDetailsEmployees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClientSubscriptionDetailsEmployees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientSubscriptionDetailsEmployees",
                table: "ClientSubscriptionDetailsEmployees",
                columns: new[] { "EmployeeId", "ClientSubscriptionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientSubscriptionDetailsEmployees",
                table: "ClientSubscriptionDetailsEmployees");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClientSubscriptionDetailsEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientSubscriptionDetailsEmployees",
                table: "ClientSubscriptionDetailsEmployees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClientSubscriptionDetailsEmployee",
                columns: table => new
                {
                    ClientSelectedTeamEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClientSubscriptionDetailsClientSubscriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSubscriptionDetailsEmployee", x => new { x.ClientSelectedTeamEmployeeId, x.ClientSubscriptionDetailsClientSubscriptionId });
                    table.ForeignKey(
                        name: "FK_ClientSubscriptionDetailsEmployee_ClientSubscriptionDetails_ClientSubscriptionDetailsClientSubscriptionId",
                        column: x => x.ClientSubscriptionDetailsClientSubscriptionId,
                        principalTable: "ClientSubscriptionDetails",
                        principalColumn: "ClientSubscriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientSubscriptionDetailsEmployee_Employees_ClientSelectedTeamEmployeeId",
                        column: x => x.ClientSelectedTeamEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientSubscriptionDetailsEmployees_EmployeeId",
                table: "ClientSubscriptionDetailsEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSubscriptionDetailsEmployee_ClientSubscriptionDetailsClientSubscriptionId",
                table: "ClientSubscriptionDetailsEmployee",
                column: "ClientSubscriptionDetailsClientSubscriptionId");
        }
    }
}
