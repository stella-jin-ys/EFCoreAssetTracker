using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFrameworkAssetTracker.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "AssetId", "Brand", "Currency", "LocalPrice", "Model", "Office", "PriceUSD", "PurchaseDate", "Type" },
                values: new object[,]
                {
                    { 1, "MacBook", "SEK", 0.0, "A001", "Sweden", 3000.0, new DateOnly(2020, 2, 1), "Computer" },
                    { 2, "Iphone", "USD", 2000.0, "I001", "USA", 2000.0, new DateOnly(2023, 12, 1), "Phone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 2);
        }
    }
}
