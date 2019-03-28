using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.EntityModels;
using AbpCompanyName.AbpProjectName.Roles.Dto;
using AbpCompanyName.AbpProjectName.SecondContext;
using AbpCompanyName.AbpProjectName.Users.Dto;
using System.Linq;
using AbpCompanyName.AbpProjectName.FirstContext;

namespace AbpCompanyName.AbpProjectName
{
    [DependsOn(typeof(AbpProjectNameCoreModule), typeof(AbpAutoMapperModule))]
    public class AbpProjectNameApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<EntityForFirstDB, EntityForFirstDBListDto>()
                        .ForMember(t => t.MasterCreatedUserName, op => op.MapFrom(u => u.UserMaster.Name ?? "Master User Not Found"))
                        .ForMember(t => t.MyChildProperty1, op => op.MapFrom(u => u.EntityChildList.FirstOrDefault() != null ? u.EntityChildList.FirstOrDefault().MyChildProperty1 : 0))
                        .ForMember(t => t.ChildCreatedUserName, op => op.MapFrom(u => u.EntityChildList.FirstOrDefault() != null ? u.EntityChildList.FirstOrDefault().UserMaster.FullName : "Child User Not Found"));
            });
        }
    }
}
