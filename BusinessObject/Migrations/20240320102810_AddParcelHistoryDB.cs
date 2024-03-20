using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class AddParcelHistoryDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParcelHistory_Addresses_AddressId",
                table: "ParcelHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ParcelHistory_Parcels_ParcelId",
                table: "ParcelHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParcelHistory",
                table: "ParcelHistory");

            migrationBuilder.RenameTable(
                name: "ParcelHistory",
                newName: "ParcelHistories");

            migrationBuilder.RenameIndex(
                name: "IX_ParcelHistory_AddressId",
                table: "ParcelHistories",
                newName: "IX_ParcelHistories_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParcelHistories",
                table: "ParcelHistories",
                column: "ParcelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParcelHistories_Addresses_AddressId",
                table: "ParcelHistories",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParcelHistories_Parcels_ParcelId",
                table: "ParcelHistories",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParcelHistories_Addresses_AddressId",
                table: "ParcelHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ParcelHistories_Parcels_ParcelId",
                table: "ParcelHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParcelHistories",
                table: "ParcelHistories");

            migrationBuilder.RenameTable(
                name: "ParcelHistories",
                newName: "ParcelHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ParcelHistories_AddressId",
                table: "ParcelHistory",
                newName: "IX_ParcelHistory_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParcelHistory",
                table: "ParcelHistory",
                column: "ParcelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParcelHistory_Addresses_AddressId",
                table: "ParcelHistory",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParcelHistory_Parcels_ParcelId",
                table: "ParcelHistory",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
