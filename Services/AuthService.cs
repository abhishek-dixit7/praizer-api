using praizer_api.Database;

namespace praizer_api.Services;

public class AuthService
{
    public async Task<Boolean> AddUpdateUser()
    {
        await using var dbContext = new DefaultdbContext();
        
    }
}