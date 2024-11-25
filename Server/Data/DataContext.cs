using EquipmentsSystem.Shared.Models;
using EquipmentsSystem.Shared;
using EquipmentsSystem.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Type = EquipmentsSystem.Shared.Models.Type;

namespace EquipmentsSystem.Server.Data

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().HasIndex(e => e.SerialNumber);
            modelBuilder.Entity<Equipment>().HasIndex(e => e.Name);
            modelBuilder.Entity<Equipment>().HasIndex(e => e.DateOfParche);
            modelBuilder.Entity<Equipment>().HasIndex(e => e.Status);
            modelBuilder.Entity<Equipment>().HasIndex(e => e.Quantity);


            modelBuilder.Entity<Model>()
           .HasMany(m => m.Equipment)   
           .WithOne(e => e.Model)       
           .HasForeignKey(e => e.ModelId);
            
            modelBuilder.Entity<Type>()
           .HasMany(m => m.Equipment)   
           .WithOne(e => e.Type)       
           .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<Location>()
           .HasMany(m => m.Equipment)   
           .WithOne(e => e.Location)       
           .HasForeignKey(e => e.LocationId);
        }
        public DbSet<Equipment> Equipments => Set<Equipment>();
        public DbSet<Model> Models { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
