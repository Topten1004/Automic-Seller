using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sales.atomicseller.com.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Product_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AtomicService",
                table: "AtomicService");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AtomicService");

            migrationBuilder.RenameTable(
                name: "AtomicService",
                newName: "AtomicServices");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AtomicServices",
                newName: "ServiceType");

            migrationBuilder.AddColumn<int>(
                name: "AtomicServiceId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualUnitPriceExclTax",
                table: "AtomicServices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerType",
                table: "AtomicServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AtomicServices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthUnitPriceExclTax",
                table: "AtomicServices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbShippingMax",
                table: "AtomicServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceDescription",
                table: "AtomicServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "AtomicServices",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceSKU",
                table: "AtomicServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "AtomicServices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AtomicServices",
                table: "AtomicServices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Lang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnUs = table.Column<string>(name: "En-Us", type: "nvarchar(max)", nullable: true),
                    FrFr = table.Column<string>(name: "Fr-Fr", type: "nvarchar(max)", nullable: true),
                    DeDe = table.Column<string>(name: "De-De", type: "nvarchar(max)", nullable: true),
                    ZhChs = table.Column<string>(name: "Zh-Chs", type: "nvarchar(max)", nullable: true),
                    EsEs = table.Column<string>(name: "Es-Es", type: "nvarchar(max)", nullable: true),
                    ItIt = table.Column<string>(name: "It-It", type: "nvarchar(max)", nullable: true),
                    NlNl = table.Column<string>(name: "Nl-Nl", type: "nvarchar(max)", nullable: true),
                    ZhTw = table.Column<string>(name: "Zh-Tw", type: "nvarchar(max)", nullable: true),
                    ElGr = table.Column<string>(name: "El-Gr", type: "nvarchar(max)", nullable: true),
                    JaJp = table.Column<string>(name: "Ja-Jp", type: "nvarchar(max)", nullable: true),
                    PtPt = table.Column<string>(name: "Pt-Pt", type: "nvarchar(max)", nullable: true),
                    RuRu = table.Column<string>(name: "Ru-Ru", type: "nvarchar(max)", nullable: true),
                    HiIn = table.Column<string>(name: "Hi-In", type: "nvarchar(max)", nullable: true),
                    PlPl = table.Column<string>(name: "Pl-Pl", type: "nvarchar(max)", nullable: true),
                    IdId = table.Column<string>(name: "Id-Id", type: "nvarchar(max)", nullable: true),
                    ArEg = table.Column<string>(name: "Ar-Eg", type: "nvarchar(max)", nullable: true),
                    HeIl = table.Column<string>(name: "He-Il", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lang", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_AtomicServiceId",
                table: "CartItems",
                column: "AtomicServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AtomicServices_AtomicServiceId",
                table: "CartItems",
                column: "AtomicServiceId",
                principalTable: "AtomicServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AtomicServices_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "AtomicServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AtomicServices_AtomicServiceId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AtomicServices_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Lang");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_AtomicServiceId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AtomicServices",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "AtomicServiceId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "AnnualUnitPriceExclTax",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "MonthUnitPriceExclTax",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "NbShippingMax",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "ServiceDescription",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "ServiceSKU",
                table: "AtomicServices");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AtomicServices");

            migrationBuilder.RenameTable(
                name: "AtomicServices",
                newName: "AtomicService");

            migrationBuilder.RenameColumn(
                name: "ServiceType",
                table: "AtomicService",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AtomicService",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AtomicService",
                table: "AtomicService",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    TempId = table.Column<int>(type: "int", nullable: false),
                    TempId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.UniqueConstraint("AK_Product_TempId", x => x.TempId);
                    table.UniqueConstraint("AK_Product_TempId1", x => x.TempId1);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Product_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "TempId1",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
