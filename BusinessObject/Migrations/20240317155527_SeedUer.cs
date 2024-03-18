using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class SeedUer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 0; i < 10; i++)
            {
                migrationBuilder.InsertData("Users",
                    columns: new[] {
                        "Id", "FullName",
                        "UserName",
                        "Email",
                        "SecurityStamp",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",


                    },
                    values: new object[] {
                        Guid.NewGuid().ToString(),
                        "User " + i.ToString("D3"),
                        "User-" + i.ToString("D3"),
                        "email" + i.ToString("D3") + "@gmail.com",
                        Guid.NewGuid().ToString(),
                        true,
                        false,
                        false,
                        false,
                        0
                    });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
