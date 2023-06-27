using Microsoft.EntityFrameworkCore;
using praizer_api.Contracts.Requests;
using praizer_api.Database;
using praizer_api.Database.Models;
using praizer_api.Utils;

namespace praizer_api.Services
{
    public class UserService
    {
        public static async Task<List<User>> GetUserDetails()
        {
            await using var dbContext = new DefaultdbContext();
            return await dbContext.Users.ToListAsync(); 
        }

        public static async Task<User> GetUserDetailsByUid(string uid)
        {
            await using var dbContext = new DefaultdbContext();
            return dbContext.Users.FirstOrDefault(x => x.Uid.Equals(uid))!;
        }

        public static async Task<List<User>> GetUserDeatilsByName(string name)
        {
            await using var dbContext = new DefaultdbContext();
            var users =   dbContext.Users.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(name.ToLower())).ToList();
            return users;
        }

        public static async Task<User> UpdateUserDetailsByUid(UpdateUserRequest request)
        {
            await using var dbContext = new DefaultdbContext();
            var userDetails = dbContext.Users.FirstOrDefault(x => x.Uid.Equals(request.Uid));
            if (userDetails == null) { throw new Exception("No user found!"); }

            var fileName = userDetails.Id+"_"+userDetails.Email;

            var fileUrl = await new UploadToBlob().UploadFile(request.profileFile!, fileName);
            
            
            
            userDetails.FirstName=request.firstName; 
            userDetails.LastName=request.lastName;
            userDetails.Email=request.email;
            
            
            userDetails.PhotoUrl = fileUrl ?? userDetails.PhotoUrl;
            
            
            userDetails.DateOfBirth = request.dateOfBirth;
            userDetails.DateOfJoining=request.dateOfJoining;
            
            dbContext.Users.Update(userDetails);
            await dbContext.SaveChangesAsync();
            return userDetails;
        }
    }
}
