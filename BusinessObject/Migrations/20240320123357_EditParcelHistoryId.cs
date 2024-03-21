using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class EditParcelHistoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParcelHistories_Addresses_AddressId",
                table: "ParcelHistories");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "ParcelHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParcelHistories_Addresses_AddressId",
                table: "ParcelHistories",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParcelHistories_Addresses_AddressId",
                table: "ParcelHistories");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "ParcelHistories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ParcelHistories_Addresses_AddressId",
                table: "ParcelHistories",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
