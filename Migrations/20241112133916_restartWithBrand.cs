using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEV1_2024_Assignment.Migrations
{
    /// <inheritdoc />
    public partial class restartWithBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__products_AspNetUsers_BrandId",
                table: "_products");

            migrationBuilder.DropIndex(
                name: "IX__products_BrandId",
                table: "_products");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "_products");

            migrationBuilder.CreateTable(
                name: "_brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK__brands_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__brands__products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__brands_AppUserId",
                table: "_brands",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX__brands_ProductId",
                table: "_brands",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_brands");

            migrationBuilder.AddColumn<string>(
                name: "BrandId",
                table: "_products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX__products_BrandId",
                table: "_products",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK__products_AspNetUsers_BrandId",
                table: "_products",
                column: "BrandId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
