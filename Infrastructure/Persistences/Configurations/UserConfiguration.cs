using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SceletonAPI.Domain.Entities;

namespace SceletonAPI.Infrastructure.Persistences.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Email)
                .HasColumnName("Email");

            builder.Property(e => e.Phone)
                .HasColumnName("Phone");

            builder.Property(e => e.Name)
                .HasColumnName("Name");

            builder.Property(e => e.Key)
                .HasColumnName("Key");

            builder.Property(e => e.Verivy)
                .HasColumnType("bit")
                .HasColumnName("Verivy");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CreateDate");

            builder.Property(e => e.LastUpdateBy)
                .HasColumnName("LastUpdateBy")
                .HasMaxLength(50);

            builder.Property(e => e.LastUpdateDate)
                .HasColumnName("LastUpdateDate")
                .HasColumnType("datetime");
                
            builder.Property(e => e.ExpiredDate)
                .HasColumnName("ExpiredDate")
                .HasColumnType("datetime");

            builder.Property(e => e.RowStatus)
                .HasColumnName("RowStatus")
                .HasDefaultValue(0);

            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("User");
        }
    }
}
