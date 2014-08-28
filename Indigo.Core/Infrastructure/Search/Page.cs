using System;
using System.Collections.Generic;

namespace Indigo.Infrastructure.Search
{
    public class Page<T> : IPageable
    {
        private ICollection<T> records;
        private int totalRecordCount;
        private int totalPageCount;
        private int pageSize;
        private int pageNumber;

        public Page(ICollection<T> records, int totalRecordCount, int pageNumber, int pageSize)
        {
            this.records = records;
            this.totalRecordCount = totalRecordCount;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            totalPageCount = totalRecordCount / pageSize + Math.Sign(totalRecordCount % pageSize);
        }

        public ICollection<T> Records
        {
            get { return records; }
        }

        public int TotalRecordCount
        {
            get { return totalRecordCount; }
        }

        public int TotalPageCount
        {
            get { return totalPageCount; }
        }

        public int PageSize
        {
            get { return pageSize; }
        }

        public int PageNumber
        {
            get { return pageNumber; }
        }

        public int PreviousPage
        {
            get { return HasPrevious() ? pageNumber - 1 : -1; }
        }

        public int NextPage
        {
            get { return HasNext() ? pageNumber + 1 : -1; }
        }

        public bool HasPrevious()
        {
            return pageNumber > 1;
        }

        public bool HasNext()
        {
            return pageNumber < totalPageCount;
        }
    }
}
