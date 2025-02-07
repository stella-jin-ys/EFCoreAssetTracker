
using Microsoft.Identity.Client;

public class ManageAssets
{
    private readonly MyDbContext context;

    public ManageAssets()
    {
        context = new MyDbContext();
    }
    public void AddAssets()
    {
        while (true)
        {
            Console.Write("Enter the asset type, computer or phone: ");
            string assetType = Console.ReadLine().Trim();
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
            string office;
            while (true)
            {
                Console.Write("Enter the office, Sweden, Spain or USA: ");
                office = Console.ReadLine().Trim().ToLower();
                if (office != "sweden" && office != "spain" && office != "usa")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid office. Please enter 'Sweden' or 'Spain' or 'USA'.");
                    Console.ResetColor();
                }
                else break;
            }
            string currency = office switch
            {
                "sweden" => "SEK",
                "spain" => "EUR",
                _ => "USD"
            };
            double priceUSD;
            while (true)
            {
                Console.Write("Enter the asset price in USD: ");
                if (!double.TryParse(Console.ReadLine().Trim(), out priceUSD) || priceUSD < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price. Please enter a positive number.");
                    Console.ResetColor();
                }
                else break;
            }
            double localPrice = CurrencyConverter.Convert(priceUSD, "USD", currency);
            DateTime purchaseTime;
            while (true)
            {
                Console.Write("Enter the purchase date(MM-DD-YYYY): ");
                if (!DateTime.TryParse(Console.ReadLine(), out purchaseTime))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date formate. Please enter a valid date.");
                    Console.ResetColor();
                }
                else break;
            }
            DateOnly purchaseDate = DateOnly.FromDateTime(purchaseTime);

            Asset asset = new Asset
            {
                Type = assetType,
                Brand = assetBrand,
                Model = assetModel,
                Office = office,
                PriceUSD = priceUSD,
                Currency = currency,
                LocalPrice = localPrice,
                PurchaseDate = purchaseDate
            };
            context.Assets.Add(asset);
            context.SaveChanges();
            Console.WriteLine("Your data is saved to the Database!");
            Console.WriteLine("---------------------------------------");

        }
    }
    public void ShowAssets()
    {
        Sort();
        Console.WriteLine("AssetId".PadRight(10) + "Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Office".PadRight(20) + "Purchase Date".PadRight(20) + "Price in USD".PadRight(20) + "Currency".PadRight(20) + "Local Price".PadRight(20));
        int twoAndSevenYears = (int)(365 * 2.7);
        int twoAndFourYears = (int)(365 * 2.4);

        List<Asset> AssetData = context.Assets.ToList();
        foreach (Asset a in AssetData)
        {
            int validDays = DateOnly.FromDateTime(DateTime.Now).DayNumber - a.PurchaseDate.DayNumber;
            ConsoleColor color = ConsoleColor.White;

            if (validDays > twoAndSevenYears)
            {
                color = ConsoleColor.Red;
            }
            else if (validDays > twoAndFourYears && validDays <= twoAndSevenYears)
            {
                color = ConsoleColor.Yellow;
            }
            else { color = ConsoleColor.White; };
            PrintAssets(a, color);
        }
    }
    public void Sort()
    {
        List<Asset> assets = context.Assets.ToList();
        assets = assets.OrderBy(a => a.Office).ThenByDescending(a => a.PurchaseDate).ToList();
    }
    public void PrintAssets(Asset a, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(a.AssetId.ToString().PadRight(10) + Capitalize(a.Type).PadRight(20) + Capitalize(a.Brand).PadRight(20) + Capitalize(a.Model).PadRight(20) + Capitalize(a.Office).PadRight(20) + a.PurchaseDate.ToString().PadRight(20) + a.PriceUSD.ToString("0.00").PadRight(20) + a.Currency.PadRight(20) + a.LocalPrice.ToString("0.00".PadRight(20)));
        Console.ResetColor();
        Console.WriteLine("---------------------------------------");

    }
    private string Capitalize(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToUpper(input[0]) + input.Substring(1).ToLower();
    }
    public void UpdateAssets()
    {
        while (true)
        {
            Console.Write("Enter the asset ID to update: ");
            string userInput = Console.ReadLine().Trim();
            if (!int.TryParse(userInput, out int assetId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid asset ID. Please enter a valid number.");
                Console.ResetColor();
                return;
            }
            Asset asset = context.Assets.SingleOrDefault(a => a.AssetId == assetId);
            if (asset == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No asset found with ID {assetId}.");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Press Enter to keep the existing value.");
            Console.ResetColor();
            Console.Write("Enter new asset type, computer or phone: ");
            string input = Console.ReadLine().Trim();
            asset.Type = string.IsNullOrEmpty(input) ? asset.Type : input;
            Console.Write("Enter new asset brand: ");
            input = Console.ReadLine().Trim();
            asset.Brand = string.IsNullOrEmpty(input) ? asset.Brand : input;
            Console.Write("Enter new asset model: ");
            input = Console.ReadLine().Trim();
            asset.Model = string.IsNullOrEmpty(input) ? asset.Model : input;
            Console.Write("Enter new office, Sweden, Spain or USA: ");
            input = Console.ReadLine().Trim().ToLower();
            asset.Office = string.IsNullOrEmpty(input) ? asset.Office : input;
            string currency = asset.Office switch
            {
                "sweden" => "SEK",
                "spain" => "EUR",
                _ => "USD"
            };
            double priceUSD;
            while (true)
            {
                Console.Write("Enter new asset price in USD: ");
                input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input)) break;
                if (!double.TryParse(input, out priceUSD) || priceUSD < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price. Please enter a positive number.");
                    Console.ResetColor();
                }
                else
                {
                    asset.PriceUSD = priceUSD;
                    break;
                };
            }
            asset.LocalPrice = CurrencyConverter.Convert(asset.PriceUSD, "USD", currency);
            DateTime purchaseTime;
            while (true)
            {
                Console.Write("Enter new purchase date(MM-DD-YYYY): ");
                input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input)) break;
                if (!DateTime.TryParse(input, out purchaseTime))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date formate. Please enter a valid date.");
                    Console.ResetColor();
                }
                else
                {
                    asset.PurchaseDate = DateOnly.FromDateTime(purchaseTime);
                    break;
                }
            }
            context.Update(asset);
            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Asset updated successfully!");
            Console.ResetColor();
            Console.WriteLine("---------------------------------------");
            break;
        }
    }
    public void DeleteAssets()
    {
        Console.Write("Enter the asset ID to delete: ");
        string userInput = Console.ReadLine().Trim();
        if (!int.TryParse(userInput, out int assetId))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid asset ID. Please enter a valid number.");
            Console.ResetColor();
            return;
        }
        Asset asset = context.Assets.FirstOrDefault(a => a.AssetId == assetId);
        if (asset == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No asset found with ID {assetId}.");
            Console.ResetColor();
            return;
        }
        context.Remove(asset);
        context.SaveChanges();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Asset delete successfully!");
        Console.ResetColor();
        Console.WriteLine("---------------------------------------");
    }
}