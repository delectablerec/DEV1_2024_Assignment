using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEV1_2024_Assignment.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__products_AspNetUsers_BrandId1",
                table: "_products");

            migrationBuilder.DropForeignKey(
                name: "FK__purchases_AspNetUsers_CustomerId1",
                table: "_purchases");

            migrationBuilder.DropIndex(
                name: "IX__purchases_CustomerId1",
                table: "_purchases");

            migrationBuilder.DropIndex(
                name: "IX__products_BrandId1",
                table: "_products");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "_purchases");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "_purchases");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "_products");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "_products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "_purchases",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "_purchases",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "_products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BrandId1",
                table: "_products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX__purchases_CustomerId1",
                table: "_purchases",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX__products_BrandId1",
                table: "_products",
                column: "BrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK__products_AspNetUsers_BrandId1",
                table: "_products",
                column: "BrandId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__purchases_AspNetUsers_CustomerId1",
                table: "_purchases",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
