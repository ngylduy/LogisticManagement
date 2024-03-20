using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class EditParcelHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParcelHistories",
                table: "ParcelHistories");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ParcelHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParcelHistories",
                table: "ParcelHistories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelHistories_ParcelId",
                table: "ParcelHistories",
                column: "ParcelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParcelHistories",
                table: "ParcelHistories");

            migrationBuilder.DropIndex(
                name: "IX_ParcelHistories_ParcelId",
                table: "ParcelHistories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ParcelHistories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParcelHistories",
                table: "ParcelHistories",
                column: "ParcelId");
        }
    }
}
