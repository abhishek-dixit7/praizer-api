namespace praizer_api.Contracts.Responses
{
    public class CreatePraiseResponse
    {
        public int Id { get; set; }

        public int? UserPraisedId { get; set; }

        public int? PraizerId { get; set; }

        public DateTime? CreateOn { get; set; }

        public string PraizeText { get; set; } = null!;

        public int? RewardPoints { get; set; }

        public string? RecognitionType { get; set; }

    }
}
