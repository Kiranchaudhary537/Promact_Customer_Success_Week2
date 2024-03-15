using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promact.CustomerSuccess.Platform.Migrations
{
    /// <inheritdoc />
    public partial class Added_New_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "todoitems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todoitems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "applicationuser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationuser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_applicationuser_organization_TenantId",
                        column: x => x.TenantId,
                        principalTable: "organization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "clientfeedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FeedbackType = table.Column<int>(type: "integer", nullable: false),
                    DetailedFeedback = table.Column<string>(type: "text", nullable: false),
                    ActionTaken = table.Column<bool>(type: "boolean", nullable: false),
                    ClosureDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientfeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clientfeedback_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phase_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projectupdate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    GeneralUpdates = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectupdate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projectupdate_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "meetingminute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeetingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MoMLink = table.Column<string>(type: "text", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meetingminute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_meetingminute_applicationuser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_meetingminute_applicationuser_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_meetingminute_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projectresource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AllocationPercentage = table.Column<double>(type: "double precision", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectresource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projectresource_applicationuser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projectresource_applicationuser_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "applicationuser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projectresource_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approvedteam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberOfResources = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    AvailabilityPercentage = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    PhaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approvedteam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_approvedteam_phase_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "phase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationuser_TenantId",
                table: "applicationuser",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_approvedteam_PhaseId",
                table: "approvedteam",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_clientfeedback_ProjectId",
                table: "clientfeedback",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_meetingminute_CreatorId",
                table: "meetingminute",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_meetingminute_LastModifierId",
                table: "meetingminute",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_meetingminute_ProjectId",
                table: "meetingminute",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_phase_ProjectId",
                table: "phase",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_projectresource_CreatorId",
                table: "projectresource",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_projectresource_LastModifierId",
                table: "projectresource",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_projectresource_ProjectId",
                table: "projectresource",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_projectupdate_ProjectId",
                table: "projectupdate",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approvedteam");

            migrationBuilder.DropTable(
                name: "clientfeedback");

            migrationBuilder.DropTable(
                name: "meetingminute");

            migrationBuilder.DropTable(
                name: "projectresource");

            migrationBuilder.DropTable(
                name: "projectupdate");

            migrationBuilder.DropTable(
                name: "todoitems");

            migrationBuilder.DropTable(
                name: "phase");

            migrationBuilder.DropTable(
                name: "applicationuser");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "organization");
        }
    }
}
