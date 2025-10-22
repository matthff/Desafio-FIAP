using Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping;

public class AlunoContextMap : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Aluno");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome).IsRequired().HasMaxLength(100);

        builder.Property(a => a.Cpf).IsRequired().HasMaxLength(11).IsFixedLength();

        builder.Property(a => a.Email).IsRequired().HasMaxLength(200);

        builder.Property(a => a.SenhaHash).IsRequired().HasMaxLength(500);

        builder.Property(a => a.DataNascimento).IsRequired().HasColumnType("DATE");

        builder.Property(a => a.Ativo).IsRequired().HasDefaultValue(true);

        builder.Property(a => a.DataCadastro).IsRequired().HasDefaultValueSql("GETDATE()");

        builder.Property(a => a.DataAtualizacao).IsRequired().HasDefaultValueSql("GETDATE()");

        builder.HasIndex(a => a.Cpf).IsUnique();

        builder.HasIndex(a => a.Email).IsUnique();

        builder.HasIndex(a => a.Nome).HasDatabaseName("IX_Aluno_Nome");

        builder.HasIndex(a => a.Ativo).HasDatabaseName("IX_Aluno_Ativo");

        builder.HasMany(a => a.Turmas).WithMany(t => t.Alunos).UsingEntity<Matricula>();
    }
}
