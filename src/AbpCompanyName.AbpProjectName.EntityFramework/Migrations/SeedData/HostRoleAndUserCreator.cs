using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using AbpCompanyName.AbpProjectName.Authorization;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.EntityFramework;
using AbpCompanyName.AbpProjectName.FirstContext;
using Microsoft.AspNet.Identity;

namespace AbpCompanyName.AbpProjectName.Migrations.SeedData
{
    public class HostRoleAndUserCreator
    {
        private readonly AbpProjectNameDbContext _context;

        public HostRoleAndUserCreator(AbpProjectNameDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {

                adminRoleForHost = new Role
                {
                    Name = StaticRoleNames.Host.Admin,
                    DisplayName = StaticRoleNames.Host.Admin,
                    IsStatic = true
                };

                adminRoleForHost.SetNormalizedName();

                _context.Roles.Add(adminRoleForHost);
                _context.SaveChanges();

                //Grant all tenant permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new AbpProjectNameAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRoleForHost.Id
                        });
                }

                _context.SaveChanges();
            }

            //Admin user for tenancy host

            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        UserName = User.AdminUserName,
                        Name = "System",
                        Surname = "Administrator",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = new PasswordHasher().HashPassword(User.DefaultPassword)
                    });

                adminUserForHost.SetNormalizedNames();

                _context.SaveChanges();

                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();
            }

            var checkData = _context.EntityForFirstDBs.FirstOrDefault();
            if (checkData == null)
            {
                EntityForFirstDB dummyMaster = new EntityForFirstDB()
                {
                    MyProperty1 = 1,
                    MyProperty2 = 2,
                    CreatedBy = adminRoleForHost.Id
                };

                _context.EntityForFirstDBs.Add(dummyMaster);
                _context.SaveChanges();

                EntityChild dummyChilld = new EntityChild()
                {
                    MyChildProperty1 = 11,
                    MyChildProperty2 = 22,
                    CreatedBy = adminRoleForHost.Id,
                    EntityMasterTableId = dummyMaster.Id
                };

                _context.EntityChilds.Add(dummyChilld);
                _context.SaveChanges();
            }
        }
    }
}