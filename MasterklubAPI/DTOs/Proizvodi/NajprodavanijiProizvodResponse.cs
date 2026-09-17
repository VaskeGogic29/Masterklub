namespace MasterklubAPI.DTOs.Proizvodi;

public record NajprodavanijiProizvodResponse(
    int ProizvodId,
    string Naziv,
    int UkupnoProdatihKomada);
