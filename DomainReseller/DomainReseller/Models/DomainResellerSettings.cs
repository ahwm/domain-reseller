namespace DomainReseller.Models
{
    public class DomainResellerSettings
    {
        public DomainProvider Provider { get; set; }
        public string GoDaddySecretKey { get; set; } = "";
        public string GoDaddyAccessKey{ get; set; } = "";
        public string OpenSRSApiKey { get; set; } = "";
        public string OpenSRSUserName { get; set; } = "";
        public bool Sandbox { get; set; }
        public string DefaultTlds { get; set; } = ".com,.net,.us,.org,.co.uk,.co,.online,.info,.dev";
    }

    public enum DomainProvider
    {
        GoDaddy,
        OpenSRS
    }
}
