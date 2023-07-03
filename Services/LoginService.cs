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
            var check = dbContext.Users.FirstOrDefault(x => x.Email.Equals(request.Username));
            if (check != null)
            {
                throw new Exception("User already exists");
            }
            //Saving the User in DB
            var uid = CommonService.EncryptPassword(request.Username + request.FirstName + request.LastName);
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Username,
                PasswordHash = CommonService.EncryptPassword(request.Password),
                Uid = uid
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            
            // Generate a JWT token
            var token = await _firebaseService.GenerateTokenAsync(uid, user.Email);
            return new LoginResponse { Token=token,CurrentUserId=user.Uid};
        }

        public  async Task<LoginResponse> LogIn(LoginRequest request)
        {
            await using var dbContext = new DefaultdbContext();
            var user = dbContext.Users
                .FirstOrDefault(x => request.Username.Equals(x.Email) && CommonService.EncryptPassword(request.Password).Equals(x.PasswordHash));

            if (user == null)
            {
                throw new Exception(
                    "User Not Found! Please try again! Check your password or email! Please Sign Up or Login with Google if you're new here!");
            }
            
            var token = await _firebaseService.GenerateTokenAsync(user.Uid,user.Email);
            return new LoginResponse { Token=token,CurrentUserId=user.Uid};

        }
    }
}
