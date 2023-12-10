namespace Fundamental.Application.Codal.Services.Models;

public class MdpPagedResponse<T>
{
    public List<T> Result { get; set; }

    public int TotalRecords { get; set; }
}