using Microsoft.EntityFrameworkCore;


internal class MyDbContext : DbContext
{
    private readonly string connectionString;
    public MyDbContext()
    {
        connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // We tell the app to use the connectionString.
        optionsBuilder.UseSqlServer(connectionString);
    }
    public DbSet<Asset> Assets { get; set; }
    protected override void OnModelCreating(ModelBuilder ModelBuilder)
    {
        ModelBuilder.Entity<Asset>().HasData(new Asset { AssetId = 1, Type = "Computer", Brand = "MacBook", Model = "A001", Office = "Sweden", PurchaseDate = new DateOnly(2020, 2, 1), PriceUSD = 3000.00, Currency = "SEK", LocalPrice = 0 });
        ModelBuilder.Entity<Asset>().HasData(new Asset { AssetId = 2, Type = "Phone", Brand = "Iphone", Model = "I001", Office = "USA", PurchaseDate = new DateOnly(2023, 12, 1), PriceUSD = 2000.00, Currency = "USD", LocalPrice = 2000 });

    }
}