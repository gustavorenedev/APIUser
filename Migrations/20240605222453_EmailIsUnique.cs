using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFor7COMm.Migrations
{
    /// <inheritdoc />
    public partial class EmailIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TBC_User",
                type: "NVARCHAR2(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.CreateIndex(
                name: "IX_TBC_User_Email",
                table: "TBC_User",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TBC_User_Email",
                table: "TBC_User");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TBC_User",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)");
        }
    }
}
