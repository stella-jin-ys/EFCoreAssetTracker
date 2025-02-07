public class Computers : Asset
{
    public Computers(int assetId, string type, string brand, string model, string office, DateOnly purchaseDate, double PriceUSD, string currency, double localPrice) : base(assetId, type, brand, model, office, purchaseDate, PriceUSD, currency, localPrice) { }
}
public class Phone : Asset
{
    public Phone(int assetId, string type, string brand, string model, string office, DateOnly purchaseDate, double PriceUSD, string currency, double localPrice) : base(assetId, type, brand, model, office, purchaseDate, PriceUSD, currency, localPrice) { }
}
public class ManageAssets
{
    List<Asset> assets = new List<Asset>();
    MyDbContext context = new MyDbContext();
    public void AddAssets()
    {
        Asset asset1 = new Asset();
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Enter the asset type or enter Q to end the program!");
            Console.ResetColor();
            Console.Write("Enter the asset type, computer or phone: ");
            string assetType = Console.ReadLine().Trim().ToLower();
            if (assetType.ToLower() == "q") break;
            if (assetType.ToLower() != "computer" && assetType.ToLower() != "phone")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid asset type! Please enter 'computer'  or 'phone'.");
                Console.ResetColor();
                continue;
            }
            string assetBrand;
            while (true)
            {
                Console.Write("Enter the asset brand: ");
                assetBrand = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(assetBrand))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Brand cannot be empty.");
                    Console.ResetColor();
                }
                else break;
            }
            string assetModel;
            while (true)
            {
                Console.Write("Enter the asset model: ");
                assetModel = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(assetModel))
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Brand cannot be empty");
                    Console.ResetColor();
                }
                else break;
            }
        }
    }
    public void ShowAssets()
    {

    }
}