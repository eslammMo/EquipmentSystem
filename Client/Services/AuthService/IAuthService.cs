using EquipmentsSystem.Shared.Models;

namespace EquipmentsSystem.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword user);
        Task<ServiceResponse<UserView>> UpdateUser(UserView obj);

        Task<bool> IsUserAuthenticated();
        Task<PagingResponse<List<UserView>>> GetUsers(string email, int currentPageNumber = 1, int pageSize = 10);
        Task<ServiceResponse<bool>> DeleteUser(int obj);


    }
}
