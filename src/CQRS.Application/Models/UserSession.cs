using CQRS.Application.Interfaces;

namespace CQRS.Application.Models;

public class UserSession : IUserSession
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}
