using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteriorCostEstimator.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AI_Integration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "DetectedObjects");

            migrationBuilder.AddColumn<string>(
                name: "Detection_Image_Url",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Processing_Time",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "DetectedObjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Crop_Url",
                table: "DetectedObjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detection_Image_Url",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Processing_Time",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Crop_Url",
                table: "DetectedObjects");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "DetectedObjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DetectedObjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
