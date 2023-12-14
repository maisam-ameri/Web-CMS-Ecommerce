using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountCodeUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodes_Users_UserId",
                table: "DiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCodes_UserId",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DiscountCodes");

            migrationBuilder.CreateTable(
                name: "DiscountCodeUser",
                columns: table => new
                {
                    DisCodeUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountCodeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodeUser", x => x.DisCodeUserId);
                    table.ForeignKey(
                        name: "FK_DiscountCodeUser_DiscountCodes_DiscountCodeId",
                        column: x => x.DiscountCodeId,
                        principalTable: "DiscountCodes",
                        principalColumn: "DiscountCodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountCodeUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodeUser_DiscountCodeId",
                table: "DiscountCodeUser",
                column: "DiscountCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodeUser_UserId",
                table: "DiscountCodeUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountCodeUser");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "DiscountCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DiscountCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_UserId",
                table: "DiscountCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodes_Users_UserId",
                table: "DiscountCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
