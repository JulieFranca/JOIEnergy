namespace JOIEnergy.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
        public int ExpirationHours { get; set; }
    }
}
