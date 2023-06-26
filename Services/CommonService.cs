using praizer_api.Contracts.Responses;
using praizer_api.Database;
using praizer_api.Database.Models;

namespace praizer_api.Services
{
    public class CommonService
    {
        public static async Task<List<User>> GetTeamMembers(string uid)
        {
            await using var dbContext = new DefaultdbContext();
            var managerId = dbContext.Users.FirstOrDefault(x=>x.Uid.Equals(uid)).ManagerId;
            var res = dbContext.Users.Where(x=>x.ManagerId == managerId).ToList();
            return res;
        }

    }
}
