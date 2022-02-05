using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiApp
{
    public class InfoContext : DbContext
    {
        public DbSet<DataBaseItemClass> ItemsDataBase { get; set; }
        public DbSet<UserBaseClass> UsersDataBase { get; set; }
        public DbSet<DataBaseNewsItemClass> NewsDataBase { get; set; }
        public InfoContext()
        {  
            Database.EnsureCreated();
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=WebsiteDataBase;Username=postgres;Password=San94iki;Encoding=UTF8");

        }
    }
}
