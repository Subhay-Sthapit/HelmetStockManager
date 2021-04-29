using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelmetStockManager.Data.Migrations
{
    public partial class Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seller = table.Column<string>(nullable: false),
                    DateOfPurchase = table.Column<DateTime>(nullable: false),
                    BillNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Helmet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_id = table.Column<int>(nullable: false),
                    HelmetName = table.Column<string>(nullable: false),
                    HelmetCode = table.Column<string>(nullable: false),
                    Descripiton = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helmet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Helmet_Category_Category_id",
                        column: x => x.Category_id,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelmetSale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    BillNumber = table.Column<string>(nullable: false),
                    DateOfSale = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelmetSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelmetSale_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelmetStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HelmetId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelmetStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelmetStock_Helmet_HelmetId",
                        column: x => x.HelmetId,
                        principalTable: "Helmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDescription",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseId = table.Column<int>(nullable: false),
                    HelmetId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseDescription_Helmet_HelmetId",
                        column: x => x.HelmetId,
                        principalTable: "Helmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDescription_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelmetSaleDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HelmetSaleId = table.Column<int>(nullable: false),
                    HelmetId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelmetSaleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelmetSaleDetail_Helmet_HelmetId",
                        column: x => x.HelmetId,
                        principalTable: "Helmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HelmetSaleDetail_HelmetSale_HelmetSaleId",
                        column: x => x.HelmetSaleId,
                        principalTable: "HelmetSale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Helmet_Category_id",
                table: "Helmet",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_HelmetSale_ClientId",
                table: "HelmetSale",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_HelmetSaleDetail_HelmetId",
                table: "HelmetSaleDetail",
                column: "HelmetId");

            migrationBuilder.CreateIndex(
                name: "IX_HelmetSaleDetail_HelmetSaleId",
                table: "HelmetSaleDetail",
                column: "HelmetSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_HelmetStock_HelmetId",
                table: "HelmetStock",
                column: "HelmetId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDescription_HelmetId",
                table: "PurchaseDescription",
                column: "HelmetId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDescription_PurchaseId",
                table: "PurchaseDescription",
                column: "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HelmetSaleDetail");

            migrationBuilder.DropTable(
                name: "HelmetStock");

            migrationBuilder.DropTable(
                name: "PurchaseDescription");

            migrationBuilder.DropTable(
                name: "HelmetSale");

            migrationBuilder.DropTable(
                name: "Helmet");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
