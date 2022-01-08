using DiaryWPF.Models.Configurations;
using DiaryWPF.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace DiaryWPF
{
    public class AplicationDbContex : DbContext
    {
        #region comments
        // Your context has been configured to use a 'AplicationDbContex' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DiaryWPF.AplicationDbContex' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AplicationDbContex' 
        // connection string in the application configuration file.
        #endregion
        public AplicationDbContex()
            : base("name=AplicationDbContex")
        {
        }
        #region comments
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        //public class MyEntity
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //}
        #endregion

        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
        }

    }

}