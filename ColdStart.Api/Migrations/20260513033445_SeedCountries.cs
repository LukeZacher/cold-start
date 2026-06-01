using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ColdStart.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Description", "Name", "PrimaryColor", "SecondaryColor" },
                values: new object[,]
                {
                    { 1, "The \"domestic\" market in this game, Avenia is the largest and richest consumer market in the world. A long history of many iconic car brands across many segments, some of which have not stood the test of time or are showing signs of falling behind. Still, Avenian brands have some of the most loyal customers and continue to push automotive technology further, although not as much in the past.", "Avenia", "#1b3a6b", "#c8102e" },
                    { 2, "Dotting the sunny coastline and sprawling hills of Gallapio are some of the most iconic, revered luxury sport sedans of all time. Many have tried, but many say none have been able to best the responsive feel and handling of Gallapian engineering, which has been at the apex of motorsport for decades. A Gallapian car and its driver are here for a good time, not a long time.", "Gallapio", "#c8102e", "#d4af37" },
                    { 3, "A tiny but mighty manufacturing powerhouse, Sivoe can do a lot more than mass-produce cars, but it's still what they're best known for. You will see quite a few Sivoen cars on Avenian roads thanks to their long history of inexpensive, value-focused, practical vehicles. Sure, some of them may look cheap and have a reputation as beater cars that just won't quit, but recent models from the biggest Sivoen brands show enhanced, modern styling and premium interiors while still keeping the prices competitive, and some compelling luxury brands have even started popping up.", "Sivoe", "#cc0000", "#f0f0f0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
