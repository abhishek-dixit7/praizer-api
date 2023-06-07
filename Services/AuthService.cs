using FirebaseAdmin.Auth;
using praizer_api.Database;
using praizer_api.Database.Models;

namespace praizer_api.Services;

public static class AuthService
{
    public static async Task<Boolean> AddUpdateUser(UserRecord userRecord)
    {
        await using var dbContext = new DefaultdbContext();

        if (dbContext.Users.FirstOrDefault(x => x.Uid == userRecord.Uid) != null)
            return false;
        
        var name = userRecord.DisplayName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


        var user = new User
        {
            FirstName = name[0],
            LastName = name.Length > 1 ? name[1] : string.Empty,
            Uid = userRecord.Uid,
            Email = userRecord.Email,
            PhotoUrl = userRecord.PhotoUrl
        };
        
        dbContext.Users.Add(user);
        var result = await dbContext.SaveChangesAsync();
        return result == 1 ? true : false;
    }
}