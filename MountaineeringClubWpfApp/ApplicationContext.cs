using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MountaineeringClubWpfApp.Models;
using System.Windows;

namespace MountaineeringClubWpfApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<MountaineeringClub> MountaineeringClubs { get; set; }
        public DbSet<Mountaineer> Mountaineers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Mountain> Mountains { get; set; }

        public ApplicationContext()
        {
            try
            {
                //Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MountaineeringClubDB;Trusted_Connection=True;");
        }
    }
}
