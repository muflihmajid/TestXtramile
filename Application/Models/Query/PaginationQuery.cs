

using SceletonAPI.Application.Models.Query;

namespace BSI_Helpdesk_WhatsApp_Chatbot.Application.Models.Query
{
    public class PaginationQuery : BaseQueryCommand
    {
        public int PagingPage { set; get; }
        public int PagingLimit { set; get; }
        public string SortColumn { set; get; }
        public string SortType { set; get; }
        public string QuerySearch { set; get; }
    }
 
}