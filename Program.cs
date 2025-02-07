using dotenv.net;
public class Program
{
    public static void Main()
    {
        DotEnv.Load();
        ManageAssets manageAssets = new ManageAssets();
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Welcome to Asset Tracking App!");
            Console.ResetColor();
            Console.WriteLine("- 1.Show assets");
            Console.WriteLine("- 2.Add a new asset");
            Console.WriteLine("- 3.Update an asset");
            Console.WriteLine("- 4.Delete an asset");
            Console.WriteLine("- 5.Exit the program");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Select an option: ");
            Console.ResetColor();
            string userInput = Console.ReadLine().Trim();
            if (userInput == "5") break;
            switch (userInput)
            {
                case "1":
                    manageAssets.ShowAssets();
                    break;
                case "2":
                    manageAssets.AddAssets();
                    break;
                case "3":
                    manageAssets.UpdateAssets();
                    break;
                case "4":
                    manageAssets.DeleteAssets();
                    break;
                default:
                    Console.WriteLine("Wrong option, please try again!");
                    break;
            }



        }

    }
}
