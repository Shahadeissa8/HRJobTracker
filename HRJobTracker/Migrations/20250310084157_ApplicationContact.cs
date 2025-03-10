using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Applicantions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Applicantions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Applicantions");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Applicantions");
        }
    }
}
