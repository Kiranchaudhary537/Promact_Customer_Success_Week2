using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promact.CustomerSuccess.Platform.Migrations
{
    /// <inheritdoc />
    public partial class updated_sprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sprint_applicationuser_CreatorId",
                table: "sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_sprint_applicationuser_LastModifierId",
                table: "sprint");

            migrationBuilder.DropIndex(
                name: "IX_sprint_CreatorId",
                table: "sprint");

            migrationBuilder.DropIndex(
                name: "IX_sprint_LastModifierId",
                table: "sprint");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "sprint");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "sprint");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "sprint");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "sprint");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "sprint");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "sprint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "sprint",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "sprint",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "sprint",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "sprint",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "sprint",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "sprint",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sprint_CreatorId",
                table: "sprint",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_sprint_LastModifierId",
                table: "sprint",
                column: "LastModifierId");

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
        }
    }
}
