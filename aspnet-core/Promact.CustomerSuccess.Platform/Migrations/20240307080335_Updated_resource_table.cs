using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promact.CustomerSuccess.Platform.Migrations
{
    /// <inheritdoc />
    public partial class Updated_resource_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projectresource_applicationuser_CreatorId",
                table: "projectresource");

            migrationBuilder.DropForeignKey(
                name: "FK_projectresource_applicationuser_LastModifierId",
                table: "projectresource");

            migrationBuilder.DropIndex(
                name: "IX_projectresource_CreatorId",
                table: "projectresource");

            migrationBuilder.DropIndex(
                name: "IX_projectresource_LastModifierId",
                table: "projectresource");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "projectresource");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "projectresource");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "projectresource");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "projectresource");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "projectresource");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "projectresource");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "projectresource",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "projectresource",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "projectresource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "projectresource",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "projectresource",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "projectresource",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_projectresource_CreatorId",
                table: "projectresource",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_projectresource_LastModifierId",
                table: "projectresource",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_projectresource_applicationuser_CreatorId",
                table: "projectresource",
                column: "CreatorId",
                principalTable: "applicationuser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_projectresource_applicationuser_LastModifierId",
                table: "projectresource",
                column: "LastModifierId",
                principalTable: "applicationuser",
                principalColumn: "Id");
        }
    }
}
