using Microsoft.EntityFrameworkCore;
using praizer_api.Database.Models;
using praizer_api.Responses;

namespace praizer_api.Services
{
    public class UserService
    {
        public async static Task<List<UserResponse>> GetUserDetails()
        {
            using (DefaultdbContext dbContext = new DefaultdbContext())
            {
                var response = await (from x in dbContext.Users
                                select new UserResponse()
                                {
                                    Id = x.Id,
                                    PointToAward = x.PointToAward,
                                    CreateOn = x.CreateOn,
                                    DateOfJoining = x.DateOfJoining,
                                    Email = x.Email,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    ModifedOn = x.ModifedOn,
                                    PointBalance = x.PointBalance,
                                }).ToListAsync();
                return response!=null? response:new List<UserResponse>();
            }

        }
    }
}
