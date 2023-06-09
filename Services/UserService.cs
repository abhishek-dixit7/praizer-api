using Microsoft.EntityFrameworkCore;
using praizer_api.Database;
using praizer_api.Database.Models;
using praizer_api.Responses;

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
            return dbContext.Users.Where(x => x.Uid.Equals(uid)).FirstOrDefault();
        }
    }
}
