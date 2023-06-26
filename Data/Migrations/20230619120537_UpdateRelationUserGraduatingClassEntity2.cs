using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminMNS.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationUserGraduatingClassEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "GraduatingClass",
                newName: "GraduatingClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GraduatingClassId",
                table: "GraduatingClass",
                newName: "Id");
        }
    }
}
