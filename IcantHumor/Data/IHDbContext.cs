using IcantHumor.Models;
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

            //for (int i = 1; i <= 40; i++)
            //{
            //    modelBuilder.Entity<MediaViewModel>().HasData(new MediaViewModel
            //    {
            //        Id = Guid.NewGuid(),
            //        IdOfCreator = Guid.Parse("01001B92-A37E-48F5-4CCF-08DBE68EB722"),
            //        Title = $"funny cat {i}",
            //        UrlToFile = "PostResources\\bebis.jpg",
            //        TypeOfFile = Models.Enums.TypeOfFile.Img,
            //        DateUpload = DateTime.Now,
            //    });
            //}
        }

        public DbSet<CategoryViewModel> Categories { get; set; }
        public DbSet<UserViewModel> Users { get; set; }
        public DbSet<FullUserInfoViewModel> FullInfoUsers { get; set; }
        public DbSet<MediaViewModel> MediaFiles { get; set; }
        public DbSet<ReactedUserViewModel> ReactedUsers { get; set; }
        public DbSet<FavouriteViewModel> Favourites { get; set; }
        //public DbSet<FavouriteOwnerViewModel> FavouriteOwners { get; set; }
    }
}
