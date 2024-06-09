using Microsoft.EntityFrameworkCore;
using Deli.Entities;

namespace Deli.DATA;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }


    public DbSet<AppUser> Users { get; set; }
    


    // here to add
public DbSet<QualityTools> QualityToolss { get; set; }
public DbSet<MileStone> MileStones { get; set; }
public DbSet<OurMission> OurMissions { get; set; }
public DbSet<DeliDifference> DeliDifferences { get; set; }
public DbSet<Review> Reviews { get; set; }
public DbSet<Sale> Sales { get; set; }
public DbSet<Liked> Likeds { get; set; }
public DbSet<Wishlist> Wishlists { get; set; }
public DbSet<FeedBack> FeedBacks { get; set; }
public DbSet<News> Newss { get; set; }
public DbSet<Appsettings> Appsettingss { get; set; }
public DbSet<OrderItem> OrderItems { get; set; }
public DbSet<Order> Orders { get; set; }
public DbSet<Address> Addresss { get; set; }
public DbSet<Category> Categorys { get; set; }
public DbSet<Item> Items { get; set; }
public DbSet<Inventory> Inventorys { get; set; }
public DbSet<Governorate> Governorates { get; set; }
public DbSet<Message> Messages { get; set; }
    public DbSet<Notification> Notifications { get; set; }


    public virtual async Task<int> SaveChangesAsync(Guid? userId = null)
    {
        // await OnBeforeSaveChanges(userId);
        var result = await base.SaveChangesAsync();
        return result;
    }
}



public class DbContextOptions<T>
{
}
