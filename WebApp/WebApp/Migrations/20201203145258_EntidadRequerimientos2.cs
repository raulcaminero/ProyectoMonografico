using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class EntidadRequerimientos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requerimientos_Codigo",
                table: "Requerimientos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_Codigo",
                table: "Requerimientos",
                column: "Codigo",
                unique: true);
        }
    }
}
