using Cadastro_Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace Cadastro_Livros.DataContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options) { }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .ToTable("Autor");
            modelBuilder.Entity<Livro>()
               .ToTable("Livro");
            modelBuilder.Entity<Assunto>()
                .ToTable("Assunto");

            modelBuilder.Entity<Livro>()
                .HasKey(p => p.LivroId);
            modelBuilder.Entity<Livro>()
                .Property(p => p.Titulo)
                .IsRequired()
                .HasColumnType("varchar(40)")
                .HasMaxLength(40);
            modelBuilder.Entity<Livro>()
                .Property(p => p.Editora)
                .IsRequired()
                .HasColumnType("varchar(40)")
                .HasMaxLength(40);
            modelBuilder.Entity<Livro>()
                .Property(p => p.Edicao)
                .IsRequired();
            modelBuilder.Entity<Livro>()
                .Property(p => p.AnoPublicacao)
                .IsRequired()
                .HasColumnType("varchar(4)")
                .HasMaxLength(4);
            modelBuilder.Entity<Livro>()
                .Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasAnnotation("RegularExpression", "@\"^\\$?\\d+(\\.(\\d{2}))?$\"");
                //.HasAnnotation("DisplayFormat", "DataFormatString = \'{0:C4}\'");

            // Assunto
            modelBuilder.Entity<Assunto>()
                .HasKey(p => p.AssuntoId);
            modelBuilder.Entity<Assunto>()
                .Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            // Autor
            modelBuilder.Entity<Autor>()
                .HasKey(p => p.AutorId);
            modelBuilder.Entity<Autor>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(40)")
                .HasMaxLength(40);

            modelBuilder.Entity<Livro>()
                .HasMany(e => e.Assuntos)
                .WithMany(e => e.Livros)
                .UsingEntity(
                    "Livro_Assunto",
                    l => l.HasOne(typeof(Assunto)).WithMany().HasForeignKey("AssuntosId").HasPrincipalKey(nameof(Assunto.AssuntoId)),
                    r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivrosId").HasPrincipalKey(nameof(Livro.LivroId)),
                    j => j.HasKey("AssuntosId", "LivrosId"));

            modelBuilder.Entity<Livro>()
                .HasMany(e => e.Autores)
                .WithMany(e => e.Livros)
                .UsingEntity(
                    "Livro_Autor",
                    l => l.HasOne(typeof(Autor)).WithMany().HasForeignKey("AutoresId").HasPrincipalKey(nameof(Autor.AutorId)),
                    r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivrosId").HasPrincipalKey(nameof(Livro.LivroId)),
                    j => j.HasKey("AutoresId", "LivrosId"));


            base.OnModelCreating(modelBuilder);
        }

    }
}
