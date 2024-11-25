using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EquipmentsSystem.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context
                    , IConfiguration configuration
                  //IConfiguration configuration,
      )
        {
            _context = context;
            _configuration = configuration;
            //_configuration = configuration;
            //_httpContextAccessor = httpContextAccessor;
        }

        //public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        //public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        //public string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        //public string GetUserUnit() => _httpContextAccessor.HttpContext.User.FindFirstValue("Unit");
        //public string GetUserDepartment() => _httpContextAccessor.HttpContext.User.FindFirstValue("Department");

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "لا يوجد مستخدم بهذا الاسم";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "كلمة مرور خاطئة";
            }
            //else if (user.Disabled)
            //{
            //    response.Success = false;
            //    response.Message = "Error 409";
            //}
            else
            {
                response.Data = CreateToken(user);
                // here write code to update user by adding the last login by the ip 
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "هذا المستخدم موجود بالفعل"
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "تمت الإضافة" };
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower()
                 .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Rank", user.Rank)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var Duration = DateTime.Now.AddDays(1);
            Duration = DateTime.Now.AddDays(7);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: Duration,
                    //expires: DateTime.Now.AddDays(1),
                    //expires:DateTime.Now.AddSeconds(30),
                    //expires:DateTime.Now.AddDays(5),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "لا يوجد مستخدم بهذا الاسم"
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "تم تغيير كلمة المرور" };
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public  async Task<PagingResponse<List<UserView>>> GetUsers(int currentPageNumber = 1, int pageSize = 10, string email = null)
        {
            int maxPageSize = 1000;
            pageSize = (pageSize > 0 && pageSize <= maxPageSize) ? pageSize : pageSize;
            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;
            var dbUsers =  _context.Users.AsQueryable();
            var count = dbUsers.Count();
            var usersView = await dbUsers 
               .Select(u => new UserView
               {
                   Id = u.Id,
                   DisplayName = u.DisplayName,
                   Email = u.Email,
                   Role = u.Role,
                   Rank = u.Rank
               })
               .ToListAsync();
            var users =  usersView.OrderBy(u=>u.Id).Skip(skip).Take(take).ToList();
            return new PagingResponse<List<UserView>>(users, count, currentPageNumber, pageSize);
        }

        public async Task<ServiceResponse<UserView>> UpdateUser(UserView obj)
        {
            var user =await GetUserByEmail(obj.Email);
            if (user == null) { return new ServiceResponse<UserView>() { Success=false}; }
            if(obj.Email != user.Email)
            {
                var IsExist = await UserExists(obj.Email);
                if (IsExist) { return new ServiceResponse<UserView>() { Success = false }; }
            }
            if (user.Role == "SuperAdmin")
            {
                obj.Role = user.Role;
            }
            user.Rank = obj.Rank;
            user.Email = obj.Email;
            user.Role = obj.Role;
            user.DisplayName = obj.DisplayName;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ServiceResponse<UserView>() { Success = true };
        }

        public async Task<ServiceResponse<bool>> DeleteUser(int obj)
        {
            Console.WriteLine("id" + obj);
            var user = await _context.Users.Where(u=>u.Id == obj).FirstOrDefaultAsync();
            if (user == null) { return new ServiceResponse<bool>() { Success = false }; }
            if (user.Role == "SuperAdmin")
                return new ServiceResponse<bool>() { Success = false, Message = "2" };
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>();
        }
    }
}
