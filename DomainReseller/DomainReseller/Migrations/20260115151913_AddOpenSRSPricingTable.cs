using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainReseller.Migrations
{
    /// <inheritdoc />
    public partial class AddOpenSRSPricingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenSRSPricing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TLD = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegistrationPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RenewalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenSRSPricing", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenSRSPricing_TLD",
                table: "OpenSRSPricing",
                column: "TLD",
                unique: true);

            // Seed OpenSRS Essential pricing data
            migrationBuilder.InsertData(
                table: "OpenSRSPricing",
                columns: new[] { "TLD", "RegistrationPrice", "TransferPrice", "RenewalPrice" },
                values: new object[,]
                {
                    { ".com", 14.50m, 14.50m, 14.50m },
                    { ".org", 9.99m, 15.00m, 15.00m },
                    { ".net", 16.50m, 16.50m, 16.50m },
                    { ".info", 20.00m, 20.00m, 20.00m },
                    { ".biz", 20.00m, 20.00m, 20.00m },
                    { ".us", 17.00m, 17.00m, 17.00m },
                    { ".co", 30.00m, 30.00m, 30.00m },
                    { ".xyz", 12.00m, 12.00m, 12.00m },
                    { ".online", 5.00m, 33.00m, 33.00m },
                    { ".blog", 31.00m, 31.00m, 31.00m },
                    { ".cloud", 27.00m, 27.00m, 27.00m },
                    { ".site", 8.00m, 33.00m, 33.00m },
                    { ".space", 8.00m, 30.00m, 30.00m },
                    { ".fun", 8.00m, 35.00m, 35.00m },
                    { ".tech", 14.00m, 59.00m, 59.00m },
                    { ".website", 8.00m, 25.00m, 25.00m },
                    { ".me", 12.00m, 23.00m, 23.00m },
                    { ".store", 3.00m, 52.00m, 52.00m },
                    { ".shop", 4.00m, 40.00m, 40.00m },
                    { ".app", 21.00m, 21.00m, 21.00m },
                    { ".dev", 24.00m, 24.00m, 24.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenSRSPricing");
        }
    }
}
