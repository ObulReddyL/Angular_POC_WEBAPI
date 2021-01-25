using System.Data.Entity;

namespace ShopBridgeAPI.DataAccess
{
    public interface IModel1Container : IDbContext
    {
        DbSet<Item> Items { get; set; }
    }
}
