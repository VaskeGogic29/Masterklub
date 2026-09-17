using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masterklub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TipKorisnika = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    TipProdavca = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BrojPoena = table.Column<int>(type: "int", nullable: true),
                    UkupnoOstvarenihBodova = table.Column<int>(type: "int", nullable: true),
                    Nivo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                    table.CheckConstraint("CK_Prodavac_BrojPoena", "[BrojPoena] >= 0");
                    table.CheckConstraint("CK_Prodavac_Nivo", "[Nivo] BETWEEN 1 AND 5");
                    table.CheckConstraint("CK_Prodavac_UkupnoOstvarenihBodova", "[UkupnoOstvarenihBodova] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Nagrade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BrojPoena = table.Column<int>(type: "int", nullable: false),
                    Nivo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nagrade", x => x.Id);
                    table.CheckConstraint("CK_Nagrada_BrojPoena", "[BrojPoena] >= 0");
                    table.CheckConstraint("CK_Nagrada_Nivo", "[Nivo] BETWEEN 1 AND 5");
                });

            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BrojBodova = table.Column<int>(type: "int", nullable: false),
                    Kategorija = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.Id);
                    table.CheckConstraint("CK_Proizvod_BrojBodova", "[BrojBodova] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "NarudzbineNagrada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdavacId = table.Column<int>(type: "int", nullable: false),
                    NagradaId = table.Column<int>(type: "int", nullable: false),
                    DatumNarudzbine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojPoenaOduzetih = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NarudzbineNagrada", x => x.Id);
                    table.CheckConstraint("CK_NarudzbinaNagrade_BrojPoenaOduzetih", "[BrojPoenaOduzetih] >= 0");
                    table.ForeignKey(
                        name: "FK_NarudzbineNagrada_Korisnici_ProdavacId",
                        column: x => x.ProdavacId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NarudzbineNagrada_Nagrade_NagradaId",
                        column: x => x.NagradaId,
                        principalTable: "Nagrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prodaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdavacId = table.Column<int>(type: "int", nullable: false),
                    ProizvodId = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    DatumProdaje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojBodovaOstvarenih = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodaje", x => x.Id);
                    table.CheckConstraint("CK_Prodaja_BrojBodovaOstvarenih", "[BrojBodovaOstvarenih] >= 0");
                    table.CheckConstraint("CK_Prodaja_Kolicina", "[Kolicina] > 0");
                    table.ForeignKey(
                        name: "FK_Prodaje_Korisnici_ProdavacId",
                        column: x => x.ProdavacId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prodaje_Proizvodi_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvodi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_Email",
                table: "Korisnici",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nagrade_Naziv",
                table: "Nagrade",
                column: "Naziv",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbineNagrada_NagradaId",
                table: "NarudzbineNagrada",
                column: "NagradaId");

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbineNagrada_ProdavacId",
                table: "NarudzbineNagrada",
                column: "ProdavacId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodaje_ProdavacId",
                table: "Prodaje",
                column: "ProdavacId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodaje_ProizvodId",
                table: "Prodaje",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_Naziv",
                table: "Proizvodi",
                column: "Naziv",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NarudzbineNagrada");

            migrationBuilder.DropTable(
                name: "Prodaje");

            migrationBuilder.DropTable(
                name: "Nagrade");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Proizvodi");
        }
    }
}
