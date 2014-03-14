using WebApp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebApp.EntityFrameworkProvider.Mapping
{
    public class FileTypeMap : EntityTypeConfiguration<FileType>
    {
        public FileTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Label)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DescriptionKey)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("FileTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Label).HasColumnName("Label");
            this.Property(t => t.DescriptionKey).HasColumnName("DescriptionKey");
        }
    }
}
