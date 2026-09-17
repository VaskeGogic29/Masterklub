using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;
using Masterklub.Domain.Interfaces;
using MasterklubAPI.Auth;
using MasterklubAPI.DTOs.Auth;
using MasterklubAPI.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace MasterklubAPI.Services;

public class AuthService : IAuthService
{
    private const string PorukaNeuspesneAutentifikacije = "Neispravan email ili lozinka.";

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Korisnik> _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUnitOfWork unitOfWork,
        IPasswordHasher<Korisnik> passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IOptions<JwtSettings> jwtSettings)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var korisnik = await _unitOfWork.Korisnici.GetByEmailAsync(request.Email)
            ?? throw new AuthenticationFailedException(PorukaNeuspesneAutentifikacije);

        if (korisnik.Status != StatusEntiteta.Aktivan)
            throw new AuthenticationFailedException(PorukaNeuspesneAutentifikacije);

        var rezultatProvere = _passwordHasher.VerifyHashedPassword(korisnik, korisnik.LozinkaHash, request.Lozinka);

        if (rezultatProvere == PasswordVerificationResult.Failed)
            throw new AuthenticationFailedException(PorukaNeuspesneAutentifikacije);

        var uloga = korisnik is Administrator ? Uloge.Administrator : Uloge.Prodavac;
        var token = _jwtTokenGenerator.GenerisiToken(korisnik, uloga);

        return new LoginResponse(
            token,
            DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            korisnik.Id,
            korisnik.Email,
            uloga);
    }
}
