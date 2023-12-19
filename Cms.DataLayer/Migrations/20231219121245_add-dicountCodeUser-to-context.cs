using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class adddicountCodeUsertocontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodeUser_DiscountCodes_DiscountCodeId",
                table: "DiscountCodeUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodeUser_Users_UserId",
                table: "DiscountCodeUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCodeUser",
                table: "DiscountCodeUser");

            migrationBuilder.RenameTable(
                name: "DiscountCodeUser",
                newName: "DiscountCodeUsers");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountCodeUser_UserId",
                table: "DiscountCodeUsers",
                newName: "IX_DiscountCodeUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountCodeUser_DiscountCodeId",
                table: "DiscountCodeUsers",
                newName: "IX_DiscountCodeUsers_DiscountCodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCodeUsers",
                table: "DiscountCodeUsers",
                column: "DisCodeUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodeUsers_DiscountCodes_DiscountCodeId",
                table: "DiscountCodeUsers",
                column: "DiscountCodeId",
                principalTable: "DiscountCodes",
                principalColumn: "DiscountCodeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodeUsers_Users_UserId",
                table: "DiscountCodeUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodeUsers_DiscountCodes_DiscountCodeId",
                table: "DiscountCodeUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodeUsers_Users_UserId",
                table: "DiscountCodeUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCodeUsers",
                table: "DiscountCodeUsers");

            migrationBuilder.RenameTable(
                name: "DiscountCodeUsers",
                newName: "DiscountCodeUser");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountCodeUsers_UserId",
                table: "DiscountCodeUser",
                newName: "IX_DiscountCodeUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountCodeUsers_DiscountCodeId",
                table: "DiscountCodeUser",
                newName: "IX_DiscountCodeUser_DiscountCodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCodeUser",
                table: "DiscountCodeUser",
                column: "DisCodeUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodeUser_DiscountCodes_DiscountCodeId",
                table: "DiscountCodeUser",
                column: "DiscountCodeId",
                principalTable: "DiscountCodes",
                principalColumn: "DiscountCodeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodeUser_Users_UserId",
                table: "DiscountCodeUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
