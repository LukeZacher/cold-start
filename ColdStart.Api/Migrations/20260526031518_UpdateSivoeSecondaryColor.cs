using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColdStart.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSivoeSecondaryColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "SecondaryColor",
                value: "#c0c0c0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "SecondaryColor",
                value: "#f0f0f0");
        }
    }
}
