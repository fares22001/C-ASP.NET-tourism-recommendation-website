using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trial.Migrations
{
    /// <inheritdoc />
    public partial class hoss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBinterests_Trips_TripId",
                table: "DBinterests");

            migrationBuilder.DropIndex(
                name: "IX_DBinterests_TripId",
                table: "DBinterests");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "DBinterests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "DBinterests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DBinterests_TripId",
                table: "DBinterests",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_DBinterests_Trips_TripId",
                table: "DBinterests",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
