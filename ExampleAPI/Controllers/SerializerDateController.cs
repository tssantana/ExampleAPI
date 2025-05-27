using ExampleAPI.Converters;
using ExampleAPI.Models.Dtos;
using ExampleAPI.Store;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExampleAPI.Controllers;

[Route("api/custom-serializer")]
[ApiController]
public sealed class SerializerDateController : ControllerBase
{
    /// <summary>
    /// Retorna os dados como UTC 0. Os dados originais estavam em UTC-3.
    /// </summary>
    [HttpGet("serialize")]

    public ActionResult<IEnumerable<TransactionDto>> GetTransactions()
    {
        TransactionStore.LoadTransactions();

        var transactions = TransactionStore.GetTransactions();

        var settings = new JsonSerializerSettings
        {            
            Converters = new List<JsonConverter> { new DateTimeBrasiliaConverter() },
            Formatting = Formatting.Indented
        };

        string jsonString = JsonConvert.SerializeObject(transactions, settings);

        return Ok(transactions);
    }

    /// <summary>
    /// Retorna os dados como UTC 0. Respeitando o objeto construido.
    /// </summary>
    [HttpGet("deserialize")]
    public ActionResult<string> GetTransactionsToString()
    {
        TransactionStore.LoadTransactions();

        var transactions = TransactionStore.GetTransactions();

        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new DateTimeBrasiliaConverter() },
            Formatting = Formatting.Indented
        };

        string jsonString = JsonConvert.SerializeObject(transactions, settings);

        return Ok(jsonString);
    }
}
