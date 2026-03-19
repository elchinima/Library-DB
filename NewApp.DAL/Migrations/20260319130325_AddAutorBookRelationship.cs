using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAutorBookRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AutorId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Books_AutorId",
                table: "Books",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Autors_AutorId",
                table: "Books",
                column: "AutorId",
                principalTable: "Autors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Autors_AutorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AutorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Books");
        }
    }
}
