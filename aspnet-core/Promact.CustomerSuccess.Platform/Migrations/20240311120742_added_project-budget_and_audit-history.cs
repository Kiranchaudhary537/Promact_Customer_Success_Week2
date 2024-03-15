using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promact.CustomerSuccess.Platform.Migrations
{
    /// <inheritdoc />
    public partial class added_projectbudget_and_audithistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "phase",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "audihistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfAudit = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReviewedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Section = table.Column<string>(type: "text", nullable: false),
                    CommentQueries = table.Column<string>(type: "text", nullable: false),
                    ActionItem = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audihistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projectbudget",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    DurationInMonths = table.Column<int>(type: "integer", nullable: true),
                    BudgetedHours = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectbudget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projectbudget_applicationuser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projectbudget_applicationuser_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projectbudget_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sprint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    Goals = table.Column<string>(type: "text", nullable: false),
                    SprintNumber = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sprint_applicationuser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sprint_applicationuser_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sprint_phase_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "phase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_projectbudget_CreatorId",
                table: "projectbudget",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_projectbudget_LastModifierId",
                table: "projectbudget",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_projectbudget_ProjectId",
                table: "projectbudget",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprint_CreatorId",
                table: "Sprint",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprint_LastModifierId",
                table: "Sprint",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprint_PhaseId",
                table: "Sprint",
                column: "PhaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audihistory");

            migrationBuilder.DropTable(
                name: "projectbudget");

            migrationBuilder.DropTable(
                name: "Sprint");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "phase");
        }
    }
}
