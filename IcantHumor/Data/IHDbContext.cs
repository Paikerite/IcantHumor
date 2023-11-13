﻿using IcantHumor.Models;
using Microsoft.EntityFrameworkCore;

namespace IcantHumor.Data
{
    public class IHDbContext : DbContext
    {
        public IHDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CategoryViewModel> Categories { get; set; }
        public DbSet<UserViewModel> Users { get; set; }
        public DbSet<MediaViewModel> MediaFiles { get; set; }
        public DbSet<ReactedUserViewModel> ReactedUsers { get; set; }
        public DbSet<FavouriteViewModel> Favourites { get; set; }
        public DbSet<FavouriteOwnerViewModel> FavouriteOwners { get; set; }
    }
}
