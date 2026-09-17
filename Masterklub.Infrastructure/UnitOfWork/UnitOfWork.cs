using Masterklub.Domain.Interfaces;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;
using Masterklub.Infrastructure.Repositories;

namespace Masterklub.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MasterklubDbContext _context;

    private IAdministratorRepository? _administratori;
    private IProdavacRepository? _prodavci;
    private IProizvodRepository? _proizvodi;
    private INagradaRepository? _nagrade;
    private IProdajaRepository? _prodaje;
    private INarudzbinaNagradeRepository? _narudzbineNagrada;

    public UnitOfWork(MasterklubDbContext context)
    {
        _context = context;
    }

    public IAdministratorRepository Administratori => _administratori ??= new AdministratorRepository(_context);
    public IProdavacRepository Prodavci => _prodavci ??= new ProdavacRepository(_context);
    public IProizvodRepository Proizvodi => _proizvodi ??= new ProizvodRepository(_context);
    public INagradaRepository Nagrade => _nagrade ??= new NagradaRepository(_context);
    public IProdajaRepository Prodaje => _prodaje ??= new ProdajaRepository(_context);
    public INarudzbinaNagradeRepository NarudzbineNagrada => _narudzbineNagrada ??= new NarudzbinaNagradeRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
