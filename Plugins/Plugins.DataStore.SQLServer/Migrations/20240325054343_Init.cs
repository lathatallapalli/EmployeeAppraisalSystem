using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Plugins.DataStore.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    CompetencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    competencyType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.CompetencyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPermission = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Appraisals",
                columns: table => new
                {
                    AppraisalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraisals", x => x.AppraisalId);
                    table.ForeignKey(
                        name: "FK_Appraisals_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appraisals_Employees_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Appraisals_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleCompetencyDetails",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CompetencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCompetencyDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_RoleCompetencyDetails_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "CompetencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleCompetencyDetails_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalDetailsCompetencies",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppraisalId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    Competency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeRating = table.Column<int>(type: "int", nullable: false),
                    EmployeeFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerRating = table.Column<int>(type: "int", nullable: false),
                    ManagerFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalDetailsCompetencies", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_AppraisalDetailsCompetencies_Appraisals_AppraisalId",
                        column: x => x.AppraisalId,
                        principalTable: "Appraisals",
                        principalColumn: "AppraisalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalDetailsCompetencies_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalDetailsCompetencies_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalDetailsObjectives",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppraisalId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    Objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeRating = table.Column<int>(type: "int", nullable: false),
                    EmployeeFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerRating = table.Column<int>(type: "int", nullable: false),
                    ManagerFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalDetailsObjectives", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_AppraisalDetailsObjectives_Appraisals_AppraisalId",
                        column: x => x.AppraisalId,
                        principalTable: "Appraisals",
                        principalColumn: "AppraisalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalDetailsObjectives_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalDetailsObjectives_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Competencies",
                columns: new[] { "CompetencyId", "CompetencyName", "competencyType" },
                values: new object[,]
                {
                    { 1, "CyberSecurity", 0 },
                    { 2, "CloudComputing", 0 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AdminPermission", "Email", "EmployeeDesignation", "EmployeeName", "ManagerId", "Mobile", "Password" },
                values: new object[] { 1, 2, "jacob@gmail.com", "Sr.Dev", "Jacob", null, "654321", "jacob" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleDescription", "RoleName" },
                values: new object[,]
                {
                    { 1, "", "Sr.dev" },
                    { 2, "", "Dev" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AdminPermission", "Email", "EmployeeDesignation", "EmployeeName", "ManagerId", "Mobile", "Password" },
                values: new object[] { 10, 3, "john@gmail.com", "Dev", "John", 1, "321456", "jacob" });

            migrationBuilder.InsertData(
                table: "RoleCompetencyDetails",
                columns: new[] { "DetailId", "CompetencyId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 1 },
                    { 4, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalDetailsCompetencies_AppraisalId",
                table: "AppraisalDetailsCompetencies",
                column: "AppraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalDetailsCompetencies_EmployeeId",
                table: "AppraisalDetailsCompetencies",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalDetailsCompetencies_ManagerId",
                table: "AppraisalDetailsCompetencies",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalDetailsObjectives_AppraisalId",
                table: "AppraisalDetailsObjectives",
                column: "AppraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalDetailsObjectives_EmployeeId",
                table: "AppraisalDetailsObjectives",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalDetailsObjectives_ManagerId",
                table: "AppraisalDetailsObjectives",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_EmployeeId",
                table: "Appraisals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_EmployeeId1",
                table: "Appraisals",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_ManagerId",
                table: "Appraisals",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompetencyDetails_CompetencyId",
                table: "RoleCompetencyDetails",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompetencyDetails_RoleId",
                table: "RoleCompetencyDetails",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppraisalDetailsCompetencies");

            migrationBuilder.DropTable(
                name: "AppraisalDetailsObjectives");

            migrationBuilder.DropTable(
                name: "RoleCompetencyDetails");

            migrationBuilder.DropTable(
                name: "Appraisals");

            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
