using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainReseller.Services
{
    public interface IDomainService
    {
        public Task<(bool status, List<string> suggestions)> CheckAvailable(string domain);

        public Task<(bool status, string message)> Register(string domain);
    }
}
