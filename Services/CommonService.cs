using praizer_api.Contracts.Responses;
using praizer_api.Database;
using praizer_api.Database.Models;
using System.Security.Cryptography;
using System.Text;

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

        public static string EncryptPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-","").ToLower();
                return hashedPassword;
            }
        }

    }
}
