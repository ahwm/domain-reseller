using DomainReseller.Models;
using GodaddyWrapper.Base;
using GodaddyWrapper.Requests;
using OpenSrs;

namespace DomainReseller.Services
{
    public class DomainService(IConfiguration configuration)
    {
        private readonly DomainResellerSettings _settings = configuration.GetSection("DomainReseller")?.Get<DomainResellerSettings>() ?? new DomainResellerSettings();

        public async Task<(bool status, List<string> suggestions)> CheckAvailable(string domain)
        {
            bool status = true;
            List<string> suggestions = [];

            if (_settings.Provider == DomainProvider.GoDaddy)
            {
                var client = new GodaddyWrapper.Client(_settings.ApiKey, _settings.GoDaddySecretKey, _settings.Sandbox ? "https://api.ote-godaddy.com/api/v1/" : "https://api.godaddy.com/api/v1/");
                try
                {
                    var response = await client.CheckDomainAvailable(new DomainAvailable
                    {
                        Domain = domain
                    });

                    status = response.Available;

                    var suggestResponse = await client.RetrieveSuggestDomain(new DomainSuggest
                    {
                        Query = domain
                    });
                    suggestions = [..suggestResponse.Select(x => x.Domain)];
                }
                catch (GodaddyException ex)
                {
                    throw new Exception(ex.StatusCode + ": " +ex.ErrorResponse.Message);
                    //Godaddy Error Message from the Godaddy API
                    //Console.WriteLine(ex.ErrorResponse.Message);
                    //Error Code
                    //Console.WriteLine(ex.StatusCode);
                }
            }
            else if (_settings.Provider == DomainProvider.OpenSRS)
            {
                var client = new OpenSrsClient(_settings.OpenSRSUserName, _settings.ApiKey, _settings.Sandbox);
                var result = await client.LookupAsync(new LookupRequest(domain));
                status = result.Status == DomainStatus.Available;

                var suggestResponse = await client.NameSuggestAsync(new NameSuggestRequest { Query = domain });
                suggestions = [..suggestResponse.Suggestions.Where(x => x.Status == "available").Select(x => x.Domain)];
            }

            return (status, suggestions);
        }
    }
}
