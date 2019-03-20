using System;
using System.Collections.Generic;
using System.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using AbpCompanyName.AbpProjectName.MigrationsOfSecond;
using AbpCompanyName.AbpProjectName.MultiTenancy;

namespace AbpCompanyName.AbpProjectName.Migrator
{
    public class MultiTenantMigrateExecuter : ITransientDependency
    {
        public Log Log { get; private set; }
        private readonly AbpSecondDbMigrator _codeMasterMigrator;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IDbPerTenantConnectionStringResolver _connectionStringResolver;
        private readonly IAbpZeroDbMigrator _migrator;

        public MultiTenantMigrateExecuter(
            IAbpZeroDbMigrator migrator, 
            AbpSecondDbMigrator cMigrator,
            IRepository<Tenant> tenantRepository,
            Log log, 
            IDbPerTenantConnectionStringResolver connectionStringResolver)
        {
            Log = log;
            _connectionStringResolver = connectionStringResolver;
            _migrator = migrator;
            _codeMasterMigrator = cMigrator;
            _tenantRepository = tenantRepository;
        }

        public void Run(bool skipConnVerification)
        {
            var hostConnStr = _connectionStringResolver.GetNameOrConnectionString(new ConnectionStringResolveArgs(MultiTenancySides.Host));
            if (hostConnStr.IsNullOrWhiteSpace())
            {
                Log.Write("Configuration file should contain a connection string named 'Default'");
                return;
            }

            Log.Write("Company database: " + hostConnStr);
            if (!skipConnVerification)
            {
                Log.Write("Continue to migration for this company database? (Y/N): ");
                var command = Console.ReadLine();
                if (!command.IsIn("Y", "y"))
                {
                    Log.Write("Migration canceled.");
                    return;
                }
            }

            Log.Write("Company database migration started...");

            try
            {
                _migrator.CreateOrMigrateForHost();
            }
            catch (Exception ex)
            {
                Log.Write("An error occured during migration of company database:");
                Log.Write(ex.ToString());
                Log.Write("Migrations Canceled.");
                return;
            }

            Log.Write("Company database migration completed.");
            Log.Write("--------------------------------------------------------");

            Log.Write("Continue to migration for CODEMASTER database?? (Y/N): ");
            var command2 = Console.ReadLine();
            if (!command2.IsIn("Y", "y"))
            {
                Log.Write("Migration canceled.");
                return;
            }

            Log.Write("CODEMASTER database migration started...");
            try
            {
                // Dummy tenant mentioning the 2nd Db connection string
                var dummyTenant = new Tenant
                {
                    Id = 1,
                    ConnectionString = ConfigurationManager.ConnectionStrings["CODEMASTEREntities"].ConnectionString,
                    TenancyName = "CodeMaster",
                    Name = "CodeMaster",
                    IsActive = true
                };

                // Passing it to the _codeMasterMigrator 
                _codeMasterMigrator.CreateOrMigrateForTenant(dummyTenant);
            }

            catch (Exception ex)
            {
                Log.Write("An error occured during migration of CODEMASTER database:");
                Log.Write(ex.ToString());
                Log.Write("Migrations Canceled.");
                return;
            }

            Log.Write("CODEMASTER database migration completed.");
            Log.Write("--------------------------------------------------------");

        }
    }
}