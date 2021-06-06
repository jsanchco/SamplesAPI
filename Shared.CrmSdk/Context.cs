using Microsoft.Extensions.Logging;
using Shared.CrmSdk.Interfaces;

namespace Shared.CrmSdk
{
    public class Context : IContext
    {
        private readonly ILogger<Context> _logger;

        public Context(ILogger<Context> logger)
        {
            _logger = logger;
        }

        public string Test(string echo)
        {
            _logger.LogInformation($"In Context[Test] -> {echo}");
            return echo;
        }
    }
}
