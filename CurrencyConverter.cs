public static class CurrencyConverter
{
    // A simple dictionary to hold currency exchange rates (for example purposes)
    private static readonly Dictionary<string, double> exchangeRates = new Dictionary<string, double>
    {
        { "SEK", 10.50 }, // Example rate for SEK (Sweden)
        { "EUR", 0.95 },  // Example rate for EUR (Spain)
        { "USD", 1.0 }    // USD to USD is 1 (base currency)
    };

    // Convert method: Converts from one currency to another
    public static double Convert(double amount, string fromCurrency, string toCurrency)
    {
        if (!exchangeRates.ContainsKey(fromCurrency) || !exchangeRates.ContainsKey(toCurrency))
        {
            throw new InvalidOperationException("Unsupported currency.");
        }

        // First, convert the amount to USD, then to the target currency
        double amountInUSD = amount / exchangeRates[fromCurrency];
        double amountInTargetCurrency = amountInUSD * exchangeRates[toCurrency];

        return amountInTargetCurrency;
    }
}