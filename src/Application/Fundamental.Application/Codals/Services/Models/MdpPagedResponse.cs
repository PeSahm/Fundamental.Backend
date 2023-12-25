namespace Fundamental.Application.Codals.Services.Models;

public class MdpPagedResponse<T>
{
    public List<T> Result { get; set; }

    public int TotalRecords { get; set; }
}