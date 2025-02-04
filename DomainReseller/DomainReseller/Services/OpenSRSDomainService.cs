using DomainReseller.Models;
using OpenSRS.NET;
using OpenSRS.NET.Actions;
using OpenSRS.NET.Models;

namespace DomainReseller.Services
{
    public sealed class OpenSRSDomainService(IConfiguration configuration, OpenSRSClient openSRS) : IDomainService
    {
        private readonly DomainResellerSettings _settings = configuration.GetSection("DomainReseller")?.Get<DomainResellerSettings>() ?? new DomainResellerSettings();

        public async Task<(bool status, List<string> suggestions)> CheckAvailable(string domain)
        {
            bool status = true;
            List<string> suggestions = [];

            var result = await openSRS.LookupAsync(new LookupRequest(domain));
            status = result.Status == DomainStatus.Available;

            var suggestResponse = await openSRS.NameSuggestAsync(new NameSuggestRequest { Query = domain, Tlds = _settings.DefaultTlds.Split(',') });
            suggestions = [.. suggestResponse.Suggestions.Where(x => x.Status == "available").Select(x => x.Domain)];

            return (status, suggestions);
        }

        public async Task<(bool status, string message)> Register(string domain)
        {
            var result = await openSRS.RegisterAsync(new RegisterRequest { 
                Domain = domain, 
                Period = 1, 
                CustomNameservers = false, 
                RegistrationType = RegistrationType.New, 
                LockDomain = true 
            });
            if (result != null)
            {
                return (true, "");
            }

            return (false, "There was a problem registering the specified domain");
        }
    }
}
