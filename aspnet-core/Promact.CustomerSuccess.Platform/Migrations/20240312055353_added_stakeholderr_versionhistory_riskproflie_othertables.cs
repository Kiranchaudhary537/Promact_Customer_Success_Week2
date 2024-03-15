using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promact.CustomerSuccess.Platform.Migrations
{
    /// <inheritdoc />
    public partial class added_stakeholderr_versionhistory_riskproflie_othertables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprint_applicationuser_CreatorId",
                table: "Sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_Sprint_applicationuser_LastModifierId",
                table: "Sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_Sprint_phase_PhaseId",
                table: "Sprint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sprint",
                table: "Sprint");

            migrationBuilder.RenameTable(
                name: "Sprint",
                newName: "sprint");

            migrationBuilder.RenameIndex(
                name: "IX_Sprint_PhaseId",
                table: "sprint",
                newName: "IX_sprint_PhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Sprint_LastModifierId",
                table: "sprint",
                newName: "IX_sprint_LastModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_Sprint_CreatorId",
                table: "sprint",
                newName: "IX_sprint_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sprint",
                table: "sprint",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "escalationmatrix",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    EscalationType = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_escalationmatrix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_escalationmatrix_applicationuser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_escalationmatrix_applicationuser_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_escalationmatrix_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "riskprofile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskType = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Impact = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_riskprofile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_riskprofile_applicationuser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_riskprofile_applicationuser_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_riskprofile_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stakeholder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ContactEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stakeholder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "versionhistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    Change = table.Column<string>(type: "text", nullable: false),
                    ChangeReason = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApprovedBy = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_versionhistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_versionhistory_applicationuser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_versionhistory_applicationuser_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_versionhistory_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "remediationstep",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RiskProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_remediationstep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_remediationstep_applicationuser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_remediationstep_applicationuser_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_remediationstep_riskprofile_RiskProfileId",
                        column: x => x.RiskProfileId,
                        principalTable: "riskprofile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_escalationmatrix_CreatorId",
                table: "escalationmatrix",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_escalationmatrix_LastModifierId",
                table: "escalationmatrix",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_escalationmatrix_ProjectId",
                table: "escalationmatrix",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_remediationstep_CreatorId",
                table: "remediationstep",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_remediationstep_LastModifierId",
                table: "remediationstep",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_remediationstep_RiskProfileId",
                table: "remediationstep",
                column: "RiskProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_riskprofile_CreatorId",
                table: "riskprofile",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_riskprofile_LastModifierId",
                table: "riskprofile",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_riskprofile_ProjectId",
                table: "riskprofile",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_versionhistory_CreatorId",
                table: "versionhistory",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_versionhistory_LastModifierId",
                table: "versionhistory",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_versionhistory_ProjectId",
                table: "versionhistory",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_sprint_applicationuser_CreatorId",
                table: "sprint",
                column: "CreatorId",
                principalTable: "applicationuser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sprint_applicationuser_LastModifierId",
                table: "sprint",
                column: "LastModifierId",
                principalTable: "applicationuser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sprint_phase_PhaseId",
                table: "sprint",
                column: "PhaseId",
                principalTable: "phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sprint_applicationuser_CreatorId",
                table: "sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_sprint_applicationuser_LastModifierId",
                table: "sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_sprint_phase_PhaseId",
                table: "sprint");

            migrationBuilder.DropTable(
                name: "escalationmatrix");

            migrationBuilder.DropTable(
                name: "remediationstep");

            migrationBuilder.DropTable(
                name: "stakeholder");

            migrationBuilder.DropTable(
                name: "versionhistory");

            migrationBuilder.DropTable(
                name: "riskprofile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sprint",
                table: "sprint");

            migrationBuilder.RenameTable(
                name: "sprint",
                newName: "Sprint");

            migrationBuilder.RenameIndex(
                name: "IX_sprint_PhaseId",
                table: "Sprint",
                newName: "IX_Sprint_PhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_sprint_LastModifierId",
                table: "Sprint",
                newName: "IX_Sprint_LastModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_sprint_CreatorId",
                table: "Sprint",
                newName: "IX_Sprint_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sprint",
                table: "Sprint",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprint_applicationuser_CreatorId",
                table: "Sprint",
                column: "CreatorId",
                principalTable: "applicationuser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprint_applicationuser_LastModifierId",
                table: "Sprint",
                column: "LastModifierId",
                principalTable: "applicationuser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprint_phase_PhaseId",
                table: "Sprint",
                column: "PhaseId",
                principalTable: "phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
