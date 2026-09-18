# Masterklub API

ASP.NET Core Web API aplikacija namenjena praćenju prodaje uređaja, sistema bodovanja prodavaca i naručivanja nagrada.

## Opis aplikacije

**Masterklub API** podržava dve korisničke uloge:

* **Administrator** — upravlja poslovnim podacima, proizvodima, nagradama i prodavcima i ima pristup administratorskim izveštajima.
* **Prodavac** — upravlja svojim profilom, pregleda proizvode i nagrade, prijavljuje ostvarene prodaje, sakuplja bodove i koristi ih za naručivanje nagrada.

Prodavac za svaku evidentiranu prodaju dobija broj bodova definisan za prodati proizvod.

Nivo prodavca određuje se na osnovu ukupno ostvarenih bodova:

* na svakih **50 bodova** nivo se povećava;
* maksimalni nivo je **5**;
* raspoloživi bodovi mogu se potrošiti na nagrade;
* ukupan istorijski broj ostvarenih bodova se čuva i koristi za određivanje nivoa.

Aplikacija je realizovana korišćenjem **ASP.NET Core Web API** i **Entity Framework Core Code First** pristupa, uz konfiguraciju entiteta preko **Fluent API-ja**.

---

## Arhitektura

Aplikacija prati višeslojnu strukturu:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
Database
```

Pri implementaciji su primenjeni:

* **Repository Pattern**
* **Unit of Work Pattern**
* **SOLID principi**
* **Clean Code / Clean Architecture principi**
* DTO modeli definisani pomoću C# `record` tipova
* asinhroni pristup podacima (`async` / `await`)
* paginacija, sortiranje i filtriranje
* autentifikacija i autorizacija zasnovana na JWT tokenima
* middleware za idempotentnost kritičnih POST operacija

Interfejsi repozitorijuma i `IUnitOfWork : IDisposable` nalaze se u **Domain** sloju, dok su njihove implementacije smeštene u **Infrastructure** sloj.

Kontroleri ne pristupaju direktno bazi niti repozitorijumima, već poslovne operacije izvršavaju preko servisnog sloja.

---

## Ključna poslovna pravila

* Prodavac može da vidi i menja samo dozvoljene podatke svog profila.
* Broj poena i nivo prodavca ne mogu se direktno menjati kroz self-service DTO.
* Bodovi se automatski dodaju nakon uspešne prijave prodaje.
* Nivo se računa na osnovu ukupno ostvarenih bodova i ne može biti veći od 5.
* Prodavac može da naruči nagradu samo ako ispunjava uslov nivoa i ima dovoljno raspoloživih bodova.
* Potrošnja bodova ne umanjuje istorijski broj ukupno ostvarenih bodova.
* Administrator ima pristup administratorskim operacijama i izveštajima.
* Administrator ne može da obriše sopstveni nalog.
* Kritične operacije prijave prodaje i naručivanja nagrade zaštićene su `Idempotency-Key` mehanizmom.

---

# Pokretanje i priprema Postmana

Pre testiranja potrebno je:

1. Kreirati i primeniti EF Core migracije i seed podatke preko **Package Manager Console-a**.
2. Pokrenuti API lokalno.
3. U Postmanu napraviti environment promenljivu:

```text
baseUrl = https://localhost:7081
```

Alternativno se može koristiti HTTP profil:

```text
baseUrl = http://localhost:5084
```

Za autentifikovane zahteve koristi se:

```http
Authorization: Bearer {{token}}
```

Za prijavu prodaje i naručivanje nagrade obavezan je i:

```http
Idempotency-Key: jedinstvena-vrednost
```

### Seed nalozi

| Uloga         | Email                 | Lozinka        |
| ------------- | --------------------- | -------------- |
| Administrator | `admin@masterklub.rs` | `Admin123!`    |
| Prodavac      | `marko@masterklub.rs` | `Prodavac123!` |
| Prodavac      | `ana@masterklub.rs`   | `Prodavac123!` |

> **Napomena:** Seed kredencijali služe isključivo za lokalno razvojno/test okruženje.

---

# Sedam ključnih slučajeva korišćenja

## 1. Autentifikacija i autorizacija korisnika

### Login administratora

**POST**

```http
{{baseUrl}}/api/auth/login
```

**Body:**

```json
{
  "email": "admin@masterklub.rs",
  "lozinka": "Admin123!"
}
```

**Očekivano:** `200 OK`

Odgovor sadrži JWT token, ID korisnika, email i ulogu `Administrator`.

Token sačuvati kao:

```text
{{adminToken}}
```

### Provera role-based autorizacije

Ulogovati Marka i sačuvati njegov token kao:

```text
{{markoToken}}
```

Zatim poslati:

**GET**

```http
{{baseUrl}}/api/administratori
```

**Header:**

```http
Authorization: Bearer {{markoToken}}
```

**Očekivano:** `403 Forbidden`

Korisnik je autentifikovan, ali nema administratorsku ulogu.

Poziv zaštićenog endpoint-a bez JWT tokena treba da vrati:

```text
401 Unauthorized
```

---

## 2. Administratorsko upravljanje proizvodima

Administrator može da kreira i menja proizvode.

### Kreiranje proizvoda

**POST**

```http
{{baseUrl}}/api/proizvodi
```

**Headers:**

```http
Authorization: Bearer {{adminToken}}
Content-Type: application/json
```

**Body:**

```json
{
  "naziv": "Frižider Beko",
  "brojBodova": 35,
  "kategorija": "AparatiZaKucu"
}
```

**Očekivano:** `201 Created`

Dobijeni ID proizvoda može se koristiti za izmenu.

### Izmena proizvoda

**PUT**

```http
{{baseUrl}}/api/proizvodi/{{proizvodId}}
```

**Body:**

```json
{
  "naziv": "Frižider Beko",
  "brojBodova": 42,
  "kategorija": "AparatiZaKucu",
  "status": "Aktivan"
}
```

**Očekivano:** `200 OK`

`brojBodova` treba da bude `42`.

---

## 3. Pregled, paginacija, sortiranje i filtriranje proizvoda

### Paginacija

**GET**

```http
{{baseUrl}}/api/proizvodi?brojStranice=1&velicinaStranice=3
```

**Header:**

```http
Authorization: Bearer {{markoToken}}
```

**Očekivano:** `200 OK`

Odgovor treba da sadrži paginiranu kolekciju i podatke o ukupnom broju elemenata i stranica.

### Sortiranje

```http
GET {{baseUrl}}/api/proizvodi?sortBy=BrojBodova&sortOpadajuce=true&brojStranice=1&velicinaStranice=10
```

Rezultat treba da bude sortiran opadajuće prema broju bodova.

### Filtriranje po kategoriji

```http
GET {{baseUrl}}/api/proizvodi/kategorija/Klime?brojStranice=1&velicinaStranice=10
```

Rezultat treba da sadrži samo proizvode iz kategorije `Klime`.

---

## 4. Upravljanje sopstvenim profilom prodavca

**GET**

```http
{{baseUrl}}/api/prodavci/moj-profil
```

**Header:**

```http
Authorization: Bearer {{markoToken}}
```

Identitet prodavca ne prosleđuje se kroz URL, već se dobija iz JWT claim-a.

Prodavac može da izmeni dozvoljene lične podatke, ali ne može direktno da promeni `brojPoena` i `nivo`.

`UpdateProdavacRequest` ne izlaže ova polja, a odgovarajuća svojstva domena dodatno su zaštićena.

Time se sprečava **mass-assignment** scenario u kojem bi prodavac pokušao sebi da dodeli bodove ili viši nivo.

---

## 5. Prijava prodaje, automatski bodovi i promena nivoa

Ovo je jedan od glavnih složenih slučajeva korišćenja.

**POST**

```http
{{baseUrl}}/api/prodaje/moje
```

**Headers:**

```http
Authorization: Bearer {{markoToken}}
Content-Type: application/json
Idempotency-Key: prodaja-marko-1
```

**Body:**

```json
{
  "proizvodId": 1,
  "kolicina": 1
}
```

**Očekivano:** `201 Created`

Bodovi se automatski obračunavaju na osnovu prodatog proizvoda.

Nakon toga:

```http
GET {{baseUrl}}/api/prodavci/moj-profil
```

Profil treba da pokaže uvećane:

* `brojPoena`
* `ukupnoOstvarenihBodova`

### Provera promene nivoa

Može se prijaviti dodatna prodaja tako da ukupan broj ostvarenih bodova pređe 50.

Tada nivo treba automatski da poraste sa `1` na `2`.

Nivo se računa prema pravilu:

```text
min((ukupnoOstvarenihBodova / 50) + 1, 5)
```

Čak i kada broj bodova odgovara nivou većem od 5, prodavac ostaje na maksimalnom nivou `5`.

---

## 6. Idempotentnost prijave prodaje

Potrebno je ponoviti potpuno isti zahtev iz prethodnog slučaja, uključujući isti:

```http
Idempotency-Key: prodaja-marko-1
```

Ponovljeni zahtev:

```http
POST {{baseUrl}}/api/prodaje/moje
```

sa istim body-jem **ne sme ponovo da evidentira prodaju niti da ponovo doda bodove**.

**Očekivano:**

```http
201 Created
Idempotency-Replayed: true
```

Middleware vraća prethodno sačuvan odgovor umesto ponovnog izvršavanja poslovne operacije.

Nakon ponovljenog zahteva proveriti profil prodavca. Broj bodova mora ostati nepromenjen.

Ovaj test demonstrira da je idempotentnost implementirana na nivou **middleware-a**, a ne kao obična provera unutar kontrolera.

---

## 7. Naručivanje nagrade i trošenje bodova

Za ovaj scenario može se koristiti nalog:

```text
ana@masterklub.rs
Prodavac123!
```

Nakon login-a token sačuvati kao:

```text
{{anaToken}}
```

### Provera nedovoljnog nivoa

**POST**

```http
{{baseUrl}}/api/narudzbine-nagrada/moje
```

**Headers:**

```http
Authorization: Bearer {{anaToken}}
Content-Type: application/json
Idempotency-Key: narudzba-ana-1
```

**Body:**

```json
{
  "nagradaId": 4
}
```

Ako nagrada zahteva nivo 5, a prodavac je nivo 1, očekuje se:

```text
409 Conflict
```

### Provera nedovoljnog broja bodova

Za nagradu nivoa 1 koja košta 10 bodova, prodavac sa 0 bodova takođe dobija:

```text
409 Conflict
```

### Uspešna narudžbina

Nakon što Ana prijavom prodaje ostvari najmanje 10 raspoloživih bodova:

**POST**

```http
{{baseUrl}}/api/narudzbine-nagrada/moje
```

**Headers:**

```http
Authorization: Bearer {{anaToken}}
Content-Type: application/json
Idempotency-Key: narudzba-ana-3
```

**Body:**

```json
{
  "nagradaId": 1
}
```

**Očekivano:** `201 Created`

Odgovor sadrži broj oduzetih poena.

Ponovnim pozivom:

```http
GET {{baseUrl}}/api/prodavci/moj-profil
```

može se proveriti da su raspoloživi bodovi umanjeni, dok `ukupnoOstvarenihBodova` ostaje nepromenjen.

Na taj način potrošnja bodova na nagrade ne utiče na istorijski rezultat na osnovu kojeg se određuje nivo.

---

# Dodatne funkcionalnosti

Pored sedam glavnih scenarija, administratorski deo API-ja podržava i poslovne izveštaje.

## TOP 5 prodavaca

```http
GET {{baseUrl}}/api/prodavci/top5
```

Dostupno samo administratoru.

Rezultat se sortira opadajuće prema ukupno ostvarenim bodovima.

## TOP 5 najprodavanijih proizvoda

```http
GET {{baseUrl}}/api/prodaje/top5-proizvoda
```

Rezultat se formira agregacijom prodate količine po proizvodu.

## Zaštita administratorskog naloga

```http
DELETE {{baseUrl}}/api/administratori/1
```

Ako administrator pokuša da obriše sopstveni nalog, očekuje se:

```text
409 Conflict
```

---

# Korišćene tehnologije i biblioteke

* **ASP.NET Core Web API** — izrada REST API-ja
* **Entity Framework Core** — ORM i Code First pristup bazi
* **EF Core Fluent API** — konfiguracija entiteta, relacija i ograničenja baze
* **JWT Bearer Authentication** — autentifikacija i role-based autorizacija korisnika
* **IMemoryCache** — čuvanje odgovora za implementaciju Idempotency middleware-a
* **ASP.NET Core Dependency Injection** — registracija servisa, repozitorijuma, Unit of Work-a i ostalih komponenti
* **Postman** — ručno i scenarijsko testiranje API endpoint-a
* **FluentValidation** — eksterna biblioteka za validaciju podataka


---

# Zaključak

**Masterklub API** demonstrira kompletan tok višeslojne ASP.NET Core aplikacije: od domenskih entiteta i EF Core pristupa podacima, preko Repository i Unit of Work obrazaca i servisnog sloja, do REST kontrolera, JWT autentifikacije, autorizacije i middleware-a.

Posebna pažnja posvećena je:

* razdvajanju odgovornosti između slojeva;
* zaštiti poslovno osetljivih podataka;
* sprovođenju poslovnih pravila u odgovarajućem sloju;
* primeni Repository Pattern-a, SOLID i Clean Architecture principa.

Cilj ovakve strukture je da kod ostane **pregledan, testabilan i jednostavan za dalje proširenje**.
