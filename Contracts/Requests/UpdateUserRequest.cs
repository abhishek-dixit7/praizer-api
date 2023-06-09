﻿namespace praizer_api.Contracts.Requests
{
    public class UpdateUserRequest
    {
        public string Uid { get; set; }
        public string   firstName { get; set; } 
        public string lastName { get; set; }
        public string email { get; set; }
        public string dateOfJoining { get; set; }
        public string dateOfBirth { get; set; }
        public string?  photoUrl { get; set; }
        public IFormFile?  profileFile { get; set; }
    }
}
