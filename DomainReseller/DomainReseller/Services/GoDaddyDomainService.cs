using DomainReseller.Models;
using GodaddyWrapper.Base;
using GodaddyWrapper.Requests;

namespace DomainReseller.Services
{
    public sealed class GoDaddyDomainService(IConfiguration configuration) : IDomainService
    {
        private readonly DomainResellerSettings _settings = configuration.GetSection("DomainReseller")?.Get<DomainResellerSettings>() ?? new DomainResellerSettings();

        public async Task<(bool status, List<string> suggestions)> CheckAvailable(string domain)
        {
            bool status = true;
            List<string> suggestions = [];

            var client = new GodaddyWrapper.Client(_settings.GoDaddyAccessKey, _settings.GoDaddySecretKey, _settings.Sandbox ? "https://api.ote-godaddy.com/api/v1/" : "https://api.godaddy.com/api/v1/");
            try
            {
                var response = await client.CheckDomainAvailable(new DomainAvailable
                {
                    Domain = domain
                });

                status = response.Available;

                var suggestResponse = await client.RetrieveSuggestDomain(new DomainSuggest
                {
                    Query = domain,
                    Tlds = []
                });
                suggestions = [..suggestResponse.Select(x => x.Domain)];
            }
            catch (GodaddyException ex)
            {
                throw new Exception(ex.StatusCode + ": " + ex.ErrorResponse.Message, ex);
            }

            return (status, suggestions);
        }

        public async Task Register(string domain)
        {
            var client = new GodaddyWrapper.Client(_settings.GoDaddyAccessKey, _settings.GoDaddySecretKey, _settings.Sandbox ? "https://api.ote-godaddy.com/api/v1/" : "https://api.godaddy.com/api/v1/");
            var result = await client.PurchaseDomain(new DomainPurchase { Domain = domain,  Period = 1 });
            if (result != null)
            {

            }
        }
    }
}
