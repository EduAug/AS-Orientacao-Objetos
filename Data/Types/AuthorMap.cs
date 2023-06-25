using AS_Orientacao_Objetos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS_Orientacao_Objetos.Data.Types
{
    public class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("authors");

            builder.Property(i=>i.Id)
            .HasColumnName("id")
            .IsRequired();
            builder.HasKey(i=>i.Id);

            builder.Property(i=>i.CPF)
            .HasColumnName("cpf")         //cpf char(11) not null
            .HasColumnType("CHAR(11)")      
            .IsRequired();

            builder.Property(i=>i.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(80)")
            .IsRequired();

            builder.Property(i=>i.Phone)
            .HasColumnName("phone")
            .HasColumnType("VARCHAR(35)")
            .IsRequired();

            builder.Property(i=>i.WriterLicense)
            .HasColumnType("license")
            .HasColumnType("VARCHAR(35)")
            .IsRequired();

            //Um autor em vários autores livros
            //onde um autor pode publicar mais de um livro
            //ou co-publicar vários livros
            //isso, porém, cabe a "join table" AutoresLivros
            builder.HasMany(a=>a.AuthorBooks)
            .WithOne(ab=>ab.Author)
            .HasForeignKey(ab=>ab.AuthorId);
        }
    }
}