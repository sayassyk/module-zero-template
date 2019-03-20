using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using AbpCompanyName.AbpProjectName.EntityFramework;
using AbpCompanyName.AbpProjectName.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace AbpCompanyName.AbpProjectName.MigrationsOfSecond
{
    public sealed class Configuration : DbMigrationsConfiguration<AbpSecondContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MigrationsOfSecond";
            ContextKey = "AbpSecondContext";
        }
        
        protected override void Seed(AbpSecondContext context)
        {

        }
    }
}
