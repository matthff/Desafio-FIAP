using Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping;

public class MatriculaContextMap : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("Matricula");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.AlunoId).IsRequired();

        builder.Property(m => m.TurmaId).IsRequired();

        builder.Property(m => m.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Ativa");

        builder.Property(m => m.DataMatricula).IsRequired().HasDefaultValueSql("GETDATE()");

        builder
            .HasOne(m => m.Aluno)
            .WithMany(a => a.Matriculas)
            .HasForeignKey(m => m.AlunoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(m => m.Turma)
            .WithMany(t => t.Matriculas)
            .HasForeignKey(m => m.TurmaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(m => new { m.AlunoId, m.TurmaId })
            .IsUnique()
            .HasDatabaseName("UQ_Matricula_Aluno_Turma");

        builder.HasIndex(m => m.AlunoId).HasDatabaseName("IX_Matricula_AlunoId");

        builder.HasIndex(m => m.TurmaId).HasDatabaseName("IX_Matricula_TurmaId");

        builder.HasIndex(m => m.Status).HasDatabaseName("IX_Matricula_Status");
    }
}
