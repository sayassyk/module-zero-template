using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using AbpCompanyName.AbpProjectName.EntityFramework;

namespace AbpCompanyName.AbpProjectName.MigrationsOfSecond
{
    public class AbpSecondDbMigrator : AbpZeroDbMigrator<AbpSecondContext, Configuration>
    {
        public AbpSecondDbMigrator(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IIocResolver iocResolver)
            : base(
                unitOfWorkManager,
                connectionStringResolver,
                iocResolver)
        {
        }
    }
}
