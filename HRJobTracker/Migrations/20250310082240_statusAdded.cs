﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class statusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Applicantions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Applicantions");
        }
    }
}
