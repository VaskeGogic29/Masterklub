using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Masterklub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DodajSeedPodatke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "Id", "Email", "Ime", "LozinkaHash", "Prezime", "Status", "TipKorisnika" },
                values: new object[] { 1, "admin@masterklub.rs", "Petar", "AQAAAAEAAYagAAAAEF8syByupmhPOCxWOv7k+9nuPtGqllrQoQQGazauFnAtVM5oknUXA8gaHgZR7tIK1Q==", "Petrović", "Aktivan", "Administrator" });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "Id", "BrojPoena", "Email", "Ime", "LozinkaHash", "Nivo", "Prezime", "Status", "TipKorisnika", "TipProdavca", "UkupnoOstvarenihBodova" },
                values: new object[,]
                {
                    { 2, 0, "marko@masterklub.rs", "Marko", "AQAAAAEAAYagAAAAEGjTLdohWuB5XfYIxllgLOOj8hB1KUCE6JuKKbi5G3KlunRZs428WUUYiIiAeYawrA==", 1, "Marković", "Aktivan", "Prodavac", "Prodavac", 0 },
                    { 3, 0, "ana@masterklub.rs", "Ana", "AQAAAAEAAYagAAAAEGjTLdohWuB5XfYIxllgLOOj8hB1KUCE6JuKKbi5G3KlunRZs428WUUYiIiAeYawrA==", 1, "Anić", "Aktivan", "Prodavac", "Prodavac", 0 },
                    { 4, 0, "jovan@masterklub.rs", "Jovan", "AQAAAAEAAYagAAAAEGjTLdohWuB5XfYIxllgLOOj8hB1KUCE6JuKKbi5G3KlunRZs428WUUYiIiAeYawrA==", 1, "Jovanović", "Aktivan", "Prodavac", "Menadzer", 0 }
                });

            migrationBuilder.InsertData(
                table: "Nagrade",
                columns: new[] { "Id", "BrojPoena", "Naziv", "Nivo", "Status" },
                values: new object[,]
                {
                    { 1, 10, "Poklon bon 2000 RSD", 1, "Aktivan" },
                    { 2, 20, "Bežične slušalice", 2, "Aktivan" },
                    { 3, 35, "Pametni sat", 3, "Aktivan" },
                    { 4, 60, "Vikend paket za dvoje", 5, "Aktivan" }
                });

            migrationBuilder.InsertData(
                table: "Proizvodi",
                columns: new[] { "Id", "BrojBodova", "Kategorija", "Naziv", "Status" },
                values: new object[,]
                {
                    { 1, 30, "TV", "Televizor Samsung 43\"", "Aktivan" },
                    { 2, 50, "TV", "Televizor LG 55\"", "Aktivan" },
                    { 3, 15, "AparatiZaKucu", "Usisivač Bosch", "Aktivan" },
                    { 4, 40, "AparatiZaKucu", "Veš mašina Gorenje", "Aktivan" },
                    { 5, 25, "Klime", "Klima Midea 12000 BTU", "Aktivan" },
                    { 6, 45, "Klime", "Klima Daikin 18000 BTU", "Aktivan" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Nagrade",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Nagrade",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Nagrade",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Nagrade",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Proizvodi",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proizvodi",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Proizvodi",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Proizvodi",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Proizvodi",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Proizvodi",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
