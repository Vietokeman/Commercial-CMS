using CMS.Core.Factories;
using CMS.Core.SeedWorks;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Data.SqlServer
{
    public class SqlServerRepositoryFactory : IRepositoryFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public SqlServerRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            // Resolve UnitOfWork từ DI container
            return _serviceProvider.GetRequiredService<IUnitOfWork>();
        }
    }
}
