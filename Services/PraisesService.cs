using praizer_api.Database.Models;
using praizer_api.Database;
using Microsoft.EntityFrameworkCore;
using praizer_api.Contracts.Requests;
using praizer_api.Contracts.Responses;

namespace praizer_api.Services
{
    public class PraisesService
    {
        public static async Task<List<GetPraisesResponse>> GetPraises()
        {
            await using var dbContext = new DefaultdbContext();
            var praises =  await dbContext.Praises.Include(p => p.UserPraised).Include(p => p.Praizer).ToListAsync();

            var responseList = praises.Select(p => new GetPraisesResponse
            {
                Id = p.Id,
                UserPraisedId = p.UserPraisedId,
                UserPraisedUid =p.UserPraised!.Uid,
                PraizerId = p.PraizerId,
                PraizerUid = p.Praizer!.Uid,
                CreateOn = p.CreateOn,
                PraizeText = p.PraizeText,
                RewardPoints = p.RewardPoints,
                RecognitionType = p.RecognitionType
            }).OrderByDescending(x=>x.CreateOn).ToList();

            return responseList;

        }

        public static async Task<CreatePraiseResponse> CreatePraise(CreatePraiseRequest request)
        {
            await using var dbContext = new DefaultdbContext();
            var praiser = dbContext.Users.FirstOrDefault(x => x.Uid == request.praiserUid);

            if (praiser == null) { throw new Exception("User does not exist - (User who is praising)"); }

            var userPraiser = dbContext.Users.FirstOrDefault(x => x.Uid == request.userPraisedUid);

            if (userPraiser == null) { throw new Exception("User does not exist - (User who is getting praising)"); }


            var praise = new Praise
            {
                PraizerId = praiser.Id,
                UserPraisedId = userPraiser.Id,
                PraizeText = request.praizeText,
                RecognitionType = request.recognitionType,
                RewardPoints = request.rewardPoints,
            };

            dbContext.Add(praise);

            praiser.PointToAward = praiser.PointToAward - request.rewardPoints;
            userPraiser.PointBalance = userPraiser.PointBalance + request.rewardPoints;

            var result = await dbContext.SaveChangesAsync();

            if (result == 0) { throw new Exception("Failed to add Praise!"); }

            var response = new CreatePraiseResponse
            {
                Id = praise.Id,
                UserPraisedId = praise.UserPraisedId,
                PraizerId = praise.PraizerId,
                PraizeText = praise.PraizeText,
                CreateOn = praise.CreateOn,
                RewardPoints = praise.RewardPoints,
                RecognitionType = praise.RecognitionType,
            };
            return response;


        }
        public static async Task<List<User>> GetBirthdayCards()
        {
            await using var dbContext = new DefaultdbContext();
            return dbContext.Users.Where(x=>x.DateOfBirth.Equals(DateOnly.FromDateTime(DateTime.Now))).ToList();
        }

        public static async Task<List<User>> GetAnniversaryCards()
        {
            await using var dbContext = new DefaultdbContext();
            return dbContext.Users.Where(x => x.DateOfJoining.Equals(DateOnly.FromDateTime(DateTime.Now))).ToList();
        }
    }
}
