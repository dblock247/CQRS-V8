namespace CQRS.Application.Helpers;

public class BasePagedRequest
{
    public int PageSize { get; set; } = 20;
    public int Page { get; set; } = 1;
    public string? SortColumn { get; set; }
    public bool SortAscending { get; set; } = true;
}
