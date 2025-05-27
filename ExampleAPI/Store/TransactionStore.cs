using ExampleAPI.Helpers;
using ExampleAPI.Models.Dtos;

namespace ExampleAPI.Store;

public sealed class TransactionStore
{
    public static List<TransactionDto> transactions = new List<TransactionDto>();

    public static IEnumerable<TransactionDto> GetTransactions()
    {
        return transactions;
    }

    public static void LoadTransactions()
    {
        var jsonFromFile = FileReader.ReadContentFile("data\\transactions.json");

        transactions = JsonDeserializer.DeserializeFromApi<List<TransactionDto>>(jsonFromFile);
    }
}
