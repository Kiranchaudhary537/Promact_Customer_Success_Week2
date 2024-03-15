using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promact.CustomerSuccess.Platform.Migrations
{
    /// <inheritdoc />
    public partial class updated_Stakeholder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "stakeholder",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_stakeholder_ProjectId",
                table: "stakeholder",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_stakeholder_project_ProjectId",
                table: "stakeholder",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stakeholder_project_ProjectId",
                table: "stakeholder");

            migrationBuilder.DropIndex(
                name: "IX_stakeholder_ProjectId",
                table: "stakeholder");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "stakeholder");
        }
    }
}
