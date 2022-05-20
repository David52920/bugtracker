using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bugtracker.Migrations
{
    public partial class FirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0218fdb4-5fce-4f93-95b8-a9852617a97e", "c54b3291-6f02-4f92-8c31-b9a5bfc3e53d", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a2019c1-1848-4cd4-b33a-7b114e788730", "e062ca34-2449-4646-87c5-51e24a97d7f1", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "57491799-74cc-4061-b793-16e85780dcdb", "dda183b4-badf-4ecc-a3c6-c2861251186d", "Member", "MEMBER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0218fdb4-5fce-4f93-95b8-a9852617a97e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a2019c1-1848-4cd4-b33a-7b114e788730");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57491799-74cc-4061-b793-16e85780dcdb");
        }
    }
}
