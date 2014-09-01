using System;
using System.Collections.Generic;

namespace Indigo.Infrastructure.Search
{
    public class Page<T> : IPageable
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;
        private readonly ICollection<T> _records;
        private readonly int _totalPageCount;
        private readonly int _totalRecordCount;

        public Page(ICollection<T> records, int totalRecordCount, int pageNumber, int pageSize)
        {
            _records = records;
            _totalRecordCount = totalRecordCount;
            _pageNumber = pageNumber;
            _pageSize = pageSize;
            _totalPageCount = totalRecordCount/pageSize + Math.Sign(totalRecordCount%pageSize);
        }

        public ICollection<T> Records
        {
            get { return _records; }
        }

        public int TotalRecordCount
        {
            get { return _totalRecordCount; }
        }

        public int TotalPageCount
        {
            get { return _totalPageCount; }
        }

        public int PageSize
        {
            get { return _pageSize; }
        }

        public int PageNumber
        {
            get { return _pageNumber; }
        }

        public int PreviousPage
        {
            get { return HasPrevious() ? _pageNumber - 1 : -1; }
        }

        public int NextPage
        {
            get { return HasNext() ? _pageNumber + 1 : -1; }
        }

        public bool HasPrevious()
        {
            return _pageNumber > 1;
        }

        public bool HasNext()
        {
            return _pageNumber < _totalPageCount;
        }
    }
}