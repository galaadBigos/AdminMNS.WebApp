using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminMNS.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateGraduatingClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartEnd",
                table: "GraduatingClass",
                newName: "EndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "GraduatingClass",
                newName: "StartEnd");
        }
    }
}
