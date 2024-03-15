using WEB.Utilities;

namespace WEB.Services.BaseServices
{
    public interface IBaseService
    {
        APIResponse responseMoodel { get; set; }
        Task<T> SendAsync<T>(APIRequest aPIRequest);
    }
}
