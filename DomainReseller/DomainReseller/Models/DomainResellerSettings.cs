﻿namespace DomainReseller.Models
{
    public class DomainResellerSettings
    {
        public DomainProvider Provider { get; set; }
        public string GoDaddySecretKey { get; set; } = "";
        public string GoDaddyAccessKey{ get; set; } = "";
        public string OpenSRSApiKey { get; set; } = "";
        public string OpenSRSUserName { get; set; } = "";
        public bool Sandbox { get; set; }
    }

    public enum DomainProvider
    {
        GoDaddy,
        OpenSRS
    }
}
