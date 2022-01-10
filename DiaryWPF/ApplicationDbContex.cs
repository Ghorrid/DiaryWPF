using DiaryWPF.Models.Configurations;
using DiaryWPF.Models.Domains;
using DiaryWPF.Properties;
using DiaryWPF.ViewModels;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace DiaryWPF
{
    public class ApplicationDbContext : DbContext
    {


        #region comments
        // Your context has been configured to use a 'ApplicationDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DiaryWPF.ApplicationDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ApplicationDbContext' 
        // connection string in the application configuration file.
        #endregion

        public ApplicationDbContext() : base(App.GetUserConnectionString())
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