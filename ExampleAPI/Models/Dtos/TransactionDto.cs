namespace ExampleAPI.Models.Dtos;

public sealed class TransactionDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}
