using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteriorCostEstimator.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DetectedObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Confidence",
                table: "DetectedObjects",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DetectedObjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confidence",
                table: "DetectedObjects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DetectedObjects");
        }
    }
}
