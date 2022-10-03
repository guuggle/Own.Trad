namespace Own.Trad.Framework.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpiryMinutes { get; set; }

        public string Secret { get; set; }
        public string SchemeName { get; set; }

    }
}