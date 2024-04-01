using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plugins.DataStore.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class AppraisalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appraisals_Employees_ManagerId",
                table: "Appraisals");

            migrationBuilder.DropIndex(
                name: "IX_Appraisals_ManagerId",
                table: "Appraisals");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId2",
                table: "Appraisals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_EmployeeId2",
                table: "Appraisals",
                column: "EmployeeId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Appraisals_Employees_EmployeeId2",
                table: "Appraisals",
                column: "EmployeeId2",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appraisals_Employees_EmployeeId2",
                table: "Appraisals");

            migrationBuilder.DropIndex(
                name: "IX_Appraisals_EmployeeId2",
                table: "Appraisals");

            migrationBuilder.DropColumn(
                name: "EmployeeId2",
                table: "Appraisals");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_ManagerId",
                table: "Appraisals",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appraisals_Employees_ManagerId",
                table: "Appraisals",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
