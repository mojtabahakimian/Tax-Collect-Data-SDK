namespace TaxCollectData.Library.Dto;

public class Pageable
{
    public Pageable(int pageNumber = 0, int pageSize = 10)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
}