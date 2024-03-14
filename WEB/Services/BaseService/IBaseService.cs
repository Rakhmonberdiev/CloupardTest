using WEB.Utilities;

namespace WEB.Services.BaseService
{
    public interface IBaseService
    {
        APIResponse responseMoodel { get; set; }
        Task<T> SendAsync<T>(APIRequest aPIRequest);
    }
}
