using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteriorCostEstimator.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Proposal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProjectId",
                table: "Proposals");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProjectId",
                table: "Proposals",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProjectId",
                table: "Proposals");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProjectId",
                table: "Proposals",
                column: "ProjectId",
                unique: true);
        }
    }
}
