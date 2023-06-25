using AS_Orientacao_Objetos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS_Orientacao_Objetos.Data.Types
{
    public class GenreMap : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("genres");

            builder.Property(i=>i.Id)
            .HasColumnName("id")
            .IsRequired();
            builder.HasKey(i=>i.Id);

            builder.Property(i=>i.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(80)")
            .IsRequired();

            builder.HasMany(j=>j.Books)
            .WithOne(k=>k.Genre)
            .HasForeignKey(k=>k.GenreId);
        }
    }
}