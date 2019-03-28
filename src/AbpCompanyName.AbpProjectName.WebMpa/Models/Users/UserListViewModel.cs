using System.Collections.Generic;
using AbpCompanyName.AbpProjectName.EntityModels;
using AbpCompanyName.AbpProjectName.Roles.Dto;
using AbpCompanyName.AbpProjectName.Users.Dto;

namespace AbpCompanyName.AbpProjectName.WebMpa.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public List<EntityForFirstDBListDto> EntityForFirstDBList { get; set; }
    }
}