namespace SceletonAPI.Application.Models.Query
{
    public class PaginationDto : BaseDto
    {
        public PaginationData Pagination { set; get; }
    }

    public class PaginationData
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int FirstRowOnPage { set; get; }
        public int LastRowOnPage { set; get; }
    }
}
