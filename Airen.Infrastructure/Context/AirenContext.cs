using Airen.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Airen.Infrastructure.Context;

public class AirenContext : DbContext {

    public DbSet<Personagem>? Personagens {get;set;}
    public DbSet<Potentia>? Potentias {get;set;}
    public DbSet<Afiliacao>? Afiliacaos {get;set;}
    public DbSet<Capitulo>? Capitulos {get;set;}
    public DbSet<Habilidade>? Habilidades {get;set;}
    public DbSet<Hobby>? Hobbies {get;set;}
    public DbSet<Parentesco>? Parentescos {get;set;}
    public DbSet<Tecnica>? Tecnicas {get;set;}
    public DbSet<Versao>? Versoes {get;set;}
    public DbSet<Volume>? Volumes {get;set;}

    private readonly IConfiguration _configuration;

    public AirenContext(DbContextOptions<AirenContext> options, IConfiguration configuration) : base(options) {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder builder) {

        builder.Entity<Parentesco>()
            .HasOne(p => p.Personagem)
            .WithMany(p => p.Parentes)
            .HasForeignKey(p => p.IdPersonagem)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Parentesco>()
            .HasOne(p => p.Parente)
            .WithMany()
            .HasForeignKey(p => p.IdParente)
            .OnDelete(DeleteBehavior.Restrict);

    }
}