using RegistrationAPI.Models;
using UserAuthentication.Models;

namespace UserAuthentication.Repository.Interface
{
    public interface IExternalCall
    {
        Task<ApiResponse<ExternalResponse>> Fetch();
    }
}