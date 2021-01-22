using Microsoft.Extensions.DependencyInjection;

namespace App.CLIghtFramework.Extensions.IOC
{
    public static class CliWindowInjection
    {
        public static ServiceCollection AddCLIghtWindowResolver(this ServiceCollection service)
        {
            // TODO: register windows that have been decorated or inherited from base window.
            
            return service;
        }
    }
}