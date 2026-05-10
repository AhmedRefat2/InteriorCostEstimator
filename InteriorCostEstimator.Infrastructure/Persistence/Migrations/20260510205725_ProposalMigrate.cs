using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteriorCostEstimator.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProposalMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ProposalItems",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ProposalItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ProposalItems");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "ProposalItems",
                newName: "Price");
        }
    }
}
