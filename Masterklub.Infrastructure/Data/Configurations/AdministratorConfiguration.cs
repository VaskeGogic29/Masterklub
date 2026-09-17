using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masterklub.Infrastructure.Data.Configurations;

public class AdministratorConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.HasData(new Administrator
        {
            Id = 1,
            Ime = "Petar",
            Prezime = "Petrović",
            Email = "admin@masterklub.rs",
            LozinkaHash = "AQAAAAEAAYagAAAAEF8syByupmhPOCxWOv7k+9nuPtGqllrQoQQGazauFnAtVM5oknUXA8gaHgZR7tIK1Q==",
            Status = StatusEntiteta.Aktivan
        });
    }
}
