using AS_Orientacao_Objetos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS_Orientacao_Objetos.Data.Types
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(i=>i.Id)
            .HasColumnName("id")
            .IsRequired();
            builder.HasKey(i=>i.Id);

            builder.Property(i=>i.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(80)")
            .IsRequired();

            builder.Property(i=>i.Phone)
            .HasColumnName("phone")
            .HasColumnType("VARCHAR(35)")
            .IsRequired();

            builder.Property(i=>i.CPF)
            .HasColumnName("cpf")
            .HasColumnType("CHAR(11)")      
            .IsRequired();


            builder.HasMany(u=>u.DonatedBooks)
            .WithOne(b=>b.DonatedBy)
            .HasForeignKey(b=>b.DonatorId);

            builder.HasMany(u=>u.UserBooks)
            .WithOne(ub=>ub.User)
            .HasForeignKey(ub=>ub.UserId);
        }
    }
}