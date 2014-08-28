namespace Indigo.Infrastructure.Search
{
    public interface IPageable
    {
        int TotalRecordCount { get; }
        int TotalPageCount { get; }
        int PageSize { get; }
        int PageNumber { get; }
        int PreviousPage { get; }
        int NextPage { get; }

        bool HasPrevious();
        bool HasNext();
    }
}
