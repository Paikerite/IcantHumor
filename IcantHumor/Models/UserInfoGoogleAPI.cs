﻿namespace IcantHumor.Models
{
    public class UserInfoGoogleAPI
    {
        public string? id { get; set; }
        public string? email { get; set; }
        public bool verified_email { get; set; }
        public string? name { get; set; }
        public string? given_name { get; set; }
        public string? picture { get; set; }
        public string? locale { get; set; }
    }
}
