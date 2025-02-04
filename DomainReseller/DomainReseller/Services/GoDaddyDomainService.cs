using DomainReseller.Models;
using GodaddyWrapper;
using GodaddyWrapper.Base;
using GodaddyWrapper.Requests;

namespace DomainReseller.Services
{
    public sealed class GoDaddyDomainService(IConfiguration configuration, GoDaddyClient goDaddyClient) : IDomainService
    {
        private readonly DomainResellerSettings _settings = configuration.GetSection("DomainReseller")?.Get<DomainResellerSettings>() ?? new DomainResellerSettings();

        public async Task<(bool status, List<string> suggestions)> CheckAvailable(string domain)
        {
            bool status = true;
            List<string> suggestions = [];

            try
            {
                var response = await goDaddyClient.CheckDomainAvailable(new DomainAvailable
                {
                    Domain = domain
                });

                status = response.Available;

                var suggestResponse = await goDaddyClient.RetrieveSuggestDomain(new DomainSuggest
                {
                    Query = domain,
                    Tlds = [.. _settings.DefaultTlds.Split(',')]
                });
                suggestions = [..suggestResponse.Select(x => x.Domain)];
            }
            catch (GodaddyException ex)
            {
                throw new Exception(ex.StatusCode + ": " + ex.ErrorResponse.Message, ex);
            }

            return (status, suggestions);
        }

        public async Task<(bool status, string message)> Register(string domain)
        {
            var result = await goDaddyClient.PurchaseDomain(new DomainPurchase { Domain = domain,  Period = 1 });
            if (result != null)
            {

                return (true, "");
            }

            return (false, "There was a problem registering the specified domain");
        }
    }
}
