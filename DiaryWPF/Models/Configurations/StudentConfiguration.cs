using DiaryWPF.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models.Configurations
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");

            HasKey(t => t.Id);

            Property(t=>t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.LastName)
              .IsRequired()
              .HasMaxLength(50);
        }
    }
}
