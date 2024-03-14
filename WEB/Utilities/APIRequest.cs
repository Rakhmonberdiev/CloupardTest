using static WEB.Utilities.ApiTypeEnum;

namespace WEB.Utilities
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string? Url { get; set; }
        public object? Data { get; set; }
    }
}
