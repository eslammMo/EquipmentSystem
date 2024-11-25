namespace EquipmentsSystem.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
        Task<ServiceResponse<UserView>> UpdateUser(UserView obj);

        //int GetUserId();
        //string GetUserEmail();
        //string GetUserRole();
        //string GetUserUnit();
        //string GetUserDepartment();
        Task<User> GetUserByEmail(string email);
        Task<ServiceResponse<bool>> DeleteUser(int obj);
        Task<PagingResponse<List<UserView>>> GetUsers(int currentPageNumber = 1, int pageSize = 10, string email = null);

    }
}
