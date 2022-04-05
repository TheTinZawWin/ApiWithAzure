using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiWithAzure.Data.Migrations
{
    public partial class adduniqueemailinmember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_Email",
                table: "Members");
        }
    }
}
