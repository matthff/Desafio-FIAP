using Api.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping;

public class TurmaContextMap : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("Turma");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nome).IsRequired().HasMaxLength(100);

        builder.Property(t => t.Descricao).IsRequired().HasMaxLength(250);

        builder.Property(t => t.Ativo).IsRequired().HasDefaultValue(true);

        builder.Property(t => t.DataCadastro).IsRequired().HasDefaultValueSql("GETDATE()");

        builder.Property(t => t.DataAtualizacao).IsRequired().HasDefaultValueSql("GETDATE()");

        builder.HasIndex(t => t.Nome).HasDatabaseName("IX_Turma_Nome");

        builder.HasIndex(t => t.Ativo).HasDatabaseName("IX_Turma_Ativo");
    }
}
