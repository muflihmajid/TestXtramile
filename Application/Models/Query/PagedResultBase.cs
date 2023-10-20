using System;
using System.Collections.Generic;

namespace SceletonAPI.Application.Models.Query
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }

        public PaginationData Pagination
        {
            get
            {
                return new PaginationData
                {
                    CurrentPage = this.CurrentPage,
                    FirstRowOnPage = this.FirstRowOnPage,
                    LastRowOnPage = this.LastRowOnPage,
                    PageCount = this.PageCount,
                    PageSize = this.PageSize,
                    RowCount = this.RowCount
                };
            }
        }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Data { get; set; }

        public PagedResult()
        {
            Data = new List<T>();
        }
    }
}
