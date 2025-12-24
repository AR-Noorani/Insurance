using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainPractice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coverages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    MinimumAllowedInvestment = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    MaximumAllowedInvestment = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    NetPercent = table.Column<decimal>(type: "decimal(10,9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentCoverages",
                columns: table => new
                {
                    CoveragesId = table.Column<long>(type: "bigint", nullable: false),
                    InvestmentsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentCoverages", x => new { x.CoveragesId, x.InvestmentsId });
                    table.ForeignKey(
                        name: "FK_InvestmentCoverages_Coverages_CoveragesId",
                        column: x => x.CoveragesId,
                        principalTable: "Coverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentCoverages_Investments_InvestmentsId",
                        column: x => x.InvestmentsId,
                        principalTable: "Investments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentCoverages_InvestmentsId",
                table: "InvestmentCoverages",
                column: "InvestmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentCoverages");

            migrationBuilder.DropTable(
                name: "Coverages");

            migrationBuilder.DropTable(
                name: "Investments");
        }
    }
}
