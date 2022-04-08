using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerOrder.CrudApi.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lge");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "lge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    Adress = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "lge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<DateTimeOffset>(nullable: true),
                    Product = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<string>(maxLength: 30, nullable: true),
                    Price = table.Column<int>(maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer",
                schema: "lge");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "lge");
        }
    }
}
