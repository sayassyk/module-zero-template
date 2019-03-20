using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using AbpCompanyName.AbpProjectName.EntityFramework;

namespace AbpCompanyName.AbpProjectName.Migrator
{
    [DependsOn(typeof(AbpProjectNameDataModule))]
    public class AbpProjectNameMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<AbpProjectNameDbContext>(null);
            Database.SetInitializer<AbpSecondContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}