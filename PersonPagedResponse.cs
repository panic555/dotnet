namespace DotNetTest.Controllers.Responses;

public class PersonPagedResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IEnumerable<PersonResponse>? Persons
    {
        get;
        set;
    }
}