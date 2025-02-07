public class Asset
{
    public int AssetId { get; set; }
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Office { get; set; }
    public double PriceUSD { get; set; }
    public string Currency { get; set; }
    public double LocalPrice { get; set; }
    public DateOnly PurchaseDate { get; set; }

    // Parameterless constructor required for EF Core
    public Asset() { }

    public Asset(int assetId, string type, string brand, string model, string office, DateOnly purchaseDate, double priceUSD, string currency, double localPrice)
    {
        AssetId = assetId;
        Type = type;
        Brand = brand;
        Model = model;
        Office = office;
        PurchaseDate = purchaseDate;
        PriceUSD = priceUSD;
        Currency = currency;
        LocalPrice = localPrice;

    }
}

