using Microsoft.EntityFrameworkCore;
using RealtyFirmAPI.Models;

namespace RealtyFirmAPI.Data
{
    public class RealtyDbContext : DbContext
    {
        public RealtyDbContext(DbContextOptions<RealtyDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListingBidder>().HasKey(lb => new { lb.Bidder_Id, lb.Listing_Id });
            modelBuilder.Entity<ListingBidder>().HasOne(lb => lb.Listing).WithMany(l => l.ListingBidders).HasForeignKey(lb => lb.Listing_Id);
            modelBuilder.Entity<ListingBidder>().HasOne(lb => lb.Bidder).WithMany(b => b.ListingBidders).HasForeignKey(lb => lb.Bidder_Id);

            modelBuilder.Entity<Listing>().HasOne(l => l.Broker).WithMany(b => b.Listings).HasForeignKey(l => l.Broker_Id);

            modelBuilder.Entity<Image>().HasOne(i => i.Listing).WithMany(l => l.Images).HasForeignKey(i => i.Listing_Id);

        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Bidder> Bidders { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingBidder> ListingBidders { get; set; }
    }
}
