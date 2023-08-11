namespace CQRS.Application.Interfaces;

public interface IUserSession
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string PhoneNumber { get; set; }
}
