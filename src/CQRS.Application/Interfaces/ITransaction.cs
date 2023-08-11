namespace CQRS.Application.Interfaces;

public interface ITransaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
}
