namespace praizer_api.Contracts.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int PointBalance { get; set; }

        public int PointToAward { get; set; }

        public DateOnly DateOfJoining { get; set; }

        public DateTime? CreateOn { get; set; }

        public DateTime? ModifedOn { get; set; }
    }
}
