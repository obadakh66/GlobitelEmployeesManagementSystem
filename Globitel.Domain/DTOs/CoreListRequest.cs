namespace Globitel.Domain.DTO
{
    public class CoreListRequest
    {
        public string SearchValue { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
