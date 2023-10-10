using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "VillasNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 10, 19, 17, 7, 889, DateTimeKind.Local).AddTicks(4793));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 10, 19, 17, 7, 889, DateTimeKind.Local).AddTicks(4810));

            migrationBuilder.CreateIndex(
                name: "IX_VillasNumbers_VillaId",
                table: "VillasNumbers",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillasNumbers_Villas_VillaId",
                table: "VillasNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillasNumbers_Villas_VillaId",
                table: "VillasNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillasNumbers_VillaId",
                table: "VillasNumbers");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "VillasNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 10, 19, 7, 45, 446, DateTimeKind.Local).AddTicks(4832));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 10, 19, 7, 45, 446, DateTimeKind.Local).AddTicks(4847));
        }
    }
}
