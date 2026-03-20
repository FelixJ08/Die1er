using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Die1Er_Projektarbeit.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thema_Berufsbereiches_berufsbereichID",
                table: "Thema");

            migrationBuilder.AddForeignKey(
                name: "FK_Thema_Berufsbereiches_berufsbereichID",
                table: "Thema",
                column: "berufsbereichID",
                principalTable: "Berufsbereiches",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thema_Berufsbereiches_berufsbereichID",
                table: "Thema");

            migrationBuilder.AddForeignKey(
                name: "FK_Thema_Berufsbereiches_berufsbereichID",
                table: "Thema",
                column: "berufsbereichID",
                principalTable: "Berufsbereiches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
