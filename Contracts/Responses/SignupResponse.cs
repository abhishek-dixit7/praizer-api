using praizer_api.Database.Models;

namespace praizer_api.Contracts.Responses
{
    public class LoginResponse
    {
        public string CurrentUserId { get; set; }
        public string Token { get; set; }
    }
}
