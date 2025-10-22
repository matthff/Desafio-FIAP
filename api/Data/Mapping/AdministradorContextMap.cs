using Api.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping;

public class AdministradorContextMap : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder.ToTable("Administrador");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome).IsRequired().HasMaxLength(200);

        builder.Property(a => a.Email).IsRequired().HasMaxLength(200);

        builder.Property(a => a.SenhaHash).IsRequired().HasMaxLength(500);

        builder.Property(a => a.Ativo).IsRequired().HasDefaultValue(true);

        builder.Property(a => a.DataCadastro).IsRequired().HasDefaultValueSql("GETDATE()");

        builder.Property(a => a.DataAtualizacao).IsRequired().HasDefaultValueSql("GETDATE()");

        builder.HasIndex(a => a.Email).IsUnique();

        builder.HasIndex(a => a.Nome).HasDatabaseName("IX_Administrador_Email");

        builder.HasIndex(a => a.Ativo).HasDatabaseName("IX_Administrador_Ativo");
    }
}
