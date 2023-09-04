using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EphemerisProvider.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlonassEphemerides",
                columns: table => new
                {
                    CsSatNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    EpochTimeUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrbitPointId = table.Column<int>(type: "INTEGER", nullable: false),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    FrequencyLitera = table.Column<int>(type: "INTEGER", nullable: false),
                    XEcefKm = table.Column<double>(type: "REAL", nullable: false),
                    YEcefKm = table.Column<double>(type: "REAL", nullable: false),
                    ZEcefKm = table.Column<double>(type: "REAL", nullable: false),
                    VxEcefKmPerSec = table.Column<double>(type: "REAL", nullable: false),
                    VyEcefKmPerSec = table.Column<double>(type: "REAL", nullable: false),
                    VzEcefKmPerSec = table.Column<double>(type: "REAL", nullable: false),
                    AxEcefKmPerSec2 = table.Column<double>(type: "REAL", nullable: false),
                    AyEcefKmPerSec2 = table.Column<double>(type: "REAL", nullable: false),
                    AzEcefKmPerSec2 = table.Column<double>(type: "REAL", nullable: false),
                    Af0Hz = table.Column<double>(type: "REAL", nullable: false),
                    Af1HzPerSec = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlonassEphemerides", x => new { x.CsSatNumber, x.EpochTimeUtc });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlonassEphemerides");
        }
    }
}
