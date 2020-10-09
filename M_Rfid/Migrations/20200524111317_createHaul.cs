using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace M_Rfid.Migrations
{
    public partial class createHaul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wagon",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Haul",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "HaulSituation",
                table: "Haul",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Haul",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DockId",
                table: "Haul",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dock",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<int>(
                name: "WagonId",
                table: "ActiveHaul",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HaulId",
                table: "ActiveHaul",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveHaulStatus",
                table: "ActiveHaul",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Haul_DockId",
                table: "Haul",
                column: "DockId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveHaul_HaulId",
                table: "ActiveHaul",
                column: "HaulId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveHaul_WagonId",
                table: "ActiveHaul",
                column: "WagonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveHaul_Haul_HaulId",
                table: "ActiveHaul",
                column: "HaulId",
                principalTable: "Haul",
                principalColumn: "HaulId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveHaul_Wagon_WagonId",
                table: "ActiveHaul",
                column: "WagonId",
                principalTable: "Wagon",
                principalColumn: "WagonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Haul_Dock_DockId",
                table: "Haul",
                column: "DockId",
                principalTable: "Dock",
                principalColumn: "DockId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveHaul_Haul_HaulId",
                table: "ActiveHaul");

            migrationBuilder.DropForeignKey(
                name: "FK_ActiveHaul_Wagon_WagonId",
                table: "ActiveHaul");

            migrationBuilder.DropForeignKey(
                name: "FK_Haul_Dock_DockId",
                table: "Haul");

            migrationBuilder.DropIndex(
                name: "IX_Haul_DockId",
                table: "Haul");

            migrationBuilder.DropIndex(
                name: "IX_ActiveHaul_HaulId",
                table: "ActiveHaul");

            migrationBuilder.DropIndex(
                name: "IX_ActiveHaul_WagonId",
                table: "ActiveHaul");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wagon",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Haul",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HaulSituation",
                table: "Haul",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Haul",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DockId",
                table: "Haul",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dock",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WagonId",
                table: "ActiveHaul",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HaulId",
                table: "ActiveHaul",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveHaulStatus",
                table: "ActiveHaul",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
