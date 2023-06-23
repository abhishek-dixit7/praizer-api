namespace praizer_api.Contracts.Requests
{
    public class CreatePraiseRequest
    {
        public int rewardPoints { get; set; }
        public string userPraisedUid { get; set; }
        public string praiserUid { get; set; }
        //public int user_praised_id { get; set; }
        public string praizeText { get; set; }
        public string recognitionType { get; set; }

    }
}
