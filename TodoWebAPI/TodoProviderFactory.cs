using Microsoft.Extensions.DependencyInjection;
using TodoWebAPI.Services;

namespace TodoWebAPI
{
    public class TodoProviderFactory(IServiceScopeFactory serviceProvider)
    {
        private readonly IServiceScopeFactory _scopeFactory = serviceProvider;

        public ITodoService GetProvider(string providerName)
        {
            // Create a new scope for each request.
            var scope = _scopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            return providerName switch
            {
                "MySql" => serviceProvider.GetRequiredService<MySqlTodoService>(),
                "MSSQL" => serviceProvider.GetRequiredService<SqlTodoService>(),
                _ => throw new ArgumentException("Invalid provider"),
            };
        }
    }
}
