using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promact.CustomerSuccess.Platform.Migrations
{
    /// <inheritdoc />
    public partial class updated_project_budget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projectbudget_applicationuser_CreatorId",
                table: "projectbudget");

            migrationBuilder.DropForeignKey(
                name: "FK_projectbudget_applicationuser_LastModifierId",
                table: "projectbudget");

            migrationBuilder.DropIndex(
                name: "IX_projectbudget_CreatorId",
                table: "projectbudget");

            migrationBuilder.DropIndex(
                name: "IX_projectbudget_LastModifierId",
                table: "projectbudget");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "projectbudget");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "projectbudget");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "projectbudget");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "projectbudget");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "projectbudget",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "projectbudget",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "projectbudget",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "projectbudget",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_projectbudget_CreatorId",
                table: "projectbudget",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_projectbudget_LastModifierId",
                table: "projectbudget",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_projectbudget_applicationuser_CreatorId",
                table: "projectbudget",
                column: "CreatorId",
                principalTable: "applicationuser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_projectbudget_applicationuser_LastModifierId",
                table: "projectbudget",
                column: "LastModifierId",
                principalTable: "applicationuser",
                principalColumn: "Id");
        }
    }
}
