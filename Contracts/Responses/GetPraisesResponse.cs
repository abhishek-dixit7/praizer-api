namespace praizer_api.Contracts.Responses
{
    public class GetPraisesResponse
    {
        public int Id { get; set; }

        public int? UserPraisedId { get; set; }
        public string UserPraisedUid { get; set; } = null!;

        public int? PraizerId { get; set; }
        public string PraizerUid { get; set; } = null!;

        public DateTime? CreateOn { get; set; }

        public string PraizeText { get; set; } = null!;

        public int? RewardPoints { get; set; }

        public string? RecognitionType { get; set; }

    }
}
