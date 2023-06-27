using praizer_api.Contracts.Requests;
using praizer_api.Database.Models;
using praizer_api.Database;
using FirebaseAdmin.Auth;
using praizer_api.Contracts.Responses;

namespace praizer_api.Services
{
    public class LoginService
    {
        private readonly FirebaseService _firebaseService;

        public LoginService(FirebaseService firebaseService)
        {
            this._firebaseService = firebaseService;
        }
        public  async Task<LoginResponse> SignUp(SignupRequest request)
        {
            await using var dbContext = new DefaultdbContext();
            string Uid = CommonService.EncryptPassword(request.Username + request.FirstName + request.LastName);
            var token = await _firebaseService.GenerateTokenAsync(Uid, request.Username);

            var check = dbContext.Users.FirstOrDefault(x => x.Email.Equals(request.Username));
            if (check != null)
            {
                throw new Exception("User already exists");
            }
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Username,
                PasswordHash = CommonService.EncryptPassword(request.Password),
                Uid = Uid
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            // Generate a JWT token
            return new LoginResponse { Token=token,CurrentUserId=user.Uid};
        }

        public  async Task<User> LoginWithUsername(LoginRequest request)
        {
            await using var dbContext = new DefaultdbContext();
            var user = dbContext.Users
                .FirstOrDefault(x => request.Username.Equals(x.Email) && CommonService.EncryptPassword(request.Password).Equals(x.PasswordHash));
            
            var token = await _firebaseService.GenerateTokenAsync(request.Username, CommonService.EncryptPassword( request.Password));
            return user;

        }
    }
}
