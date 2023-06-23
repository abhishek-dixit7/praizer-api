﻿using Microsoft.EntityFrameworkCore;
using praizer_api.Database;
using praizer_api.Database.Models;

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
    }
}
