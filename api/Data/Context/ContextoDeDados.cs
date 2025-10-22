using Api.Data.Mapping;
using Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class ContextoDeDados : DbContext
    {
        public DbSet<Administrador> Administradores { get; set; }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Turma> Turmas { get; set; }

        public DbSet<Matricula> Matriculas { get; set; }

        public ContextoDeDados(DbContextOptions<ContextoDeDados> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Administrador>(new AdministradorContextMap().Configure);
            modelBuilder.Entity<Aluno>(new AlunoContextMap().Configure);
            modelBuilder.Entity<Turma>(new TurmaContextMap().Configure);
            modelBuilder.Entity<Matricula>(new MatriculaContextMap().Configure);
        }
    }
}
