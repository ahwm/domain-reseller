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

            openSRS.Configure(_settings.OpenSRSApiKey, _settings.OpenSRSUserName, _settings.Sandbox);
            var result = await openSRS.LookupAsync(new LookupRequest(domain));
            status = result.Status == DomainStatus.Available;

            var suggestResponse = await openSRS.NameSuggestAsync(new NameSuggestRequest { Query = domain, Tlds = [] });
            suggestions = [.. suggestResponse.Suggestions.Where(x => x.Status == "available").Select(x => x.Domain)];

            return (status, suggestions);
        }

        public async Task Register(string domain)
        {
            openSRS.Configure(_settings.OpenSRSApiKey, _settings.OpenSRSUserName, _settings.Sandbox);
            var result = await openSRS.RegisterAsync(new RegisterRequest { Domain = domain, Period = 1 });
            if (result != null)
            {

            }
        }
    }
}
