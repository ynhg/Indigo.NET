namespace Indigo.Infrastructure.Search
{
    public class SearchForm
    {
        public static readonly int DefaultFirstPage = 1;
        public static readonly int DefaultPageSize = 10;

        private int _pageNumber = DefaultFirstPage;
        private int _pageSize = DefaultPageSize;

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
    }
}