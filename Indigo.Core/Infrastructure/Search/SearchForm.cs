namespace Indigo.Infrastructure.Search
{
    public class SearchForm
    {
        public const int DEFAULT_FIRST_PAGE = 1;
        public const int DEFAULT_PAGE_SIZE = 10;

        private int pageNumber = DEFAULT_FIRST_PAGE;
        private int pageSize = DEFAULT_PAGE_SIZE;

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
    }
}
