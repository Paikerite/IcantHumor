namespace IcantHumor.Models
{
    public class UserInfoDiscordAPI
    {
        public string? id { get; set; }
        public string? username { get; set; }
        public string? avatar { get; set; }
        public string? discriminator { get; set; }
        public int public_flags { get; set; }
        public int premium_type { get; set; }
        public int flags { get; set; }
        public string? banner { get; set; }
        public int accent_color { get; set; }
        public string? global_name { get; set; }
        public string? avatar_decoration_data { get; set; }
        public string? banner_color { get; set; }
        public bool mfa_enabled { get; set; }
        public string? locale { get; set; }
        public string? email { get; set; }
        public bool verified { get; set; }
    }
}
