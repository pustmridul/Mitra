using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mitra.Domain.Migrations
{
    /// <inheritdoc />
    public partial class initial14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_Streets_StreetId",
                table: "Donors");

            migrationBuilder.AlterColumn<int>(
                name: "StreetId",
                table: "Donors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_Streets_StreetId",
                table: "Donors",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_Streets_StreetId",
                table: "Donors");

            migrationBuilder.AlterColumn<int>(
                name: "StreetId",
                table: "Donors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_Streets_StreetId",
                table: "Donors",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
