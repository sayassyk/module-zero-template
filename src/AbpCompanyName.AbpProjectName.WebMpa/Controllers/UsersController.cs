using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using AbpCompanyName.AbpProjectName.Authorization;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.EntityFramework;
using AbpCompanyName.AbpProjectName.Users;
using AbpCompanyName.AbpProjectName.WebMpa.Models.Users;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.AutoMapper;
using System.Collections.Generic;
using AbpCompanyName.AbpProjectName.EntityModels;

namespace AbpCompanyName.AbpProjectName.WebMpa.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : AbpProjectNameControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly RoleManager _roleManager;

        private AbpProjectNameDbContext db = new AbpProjectNameDbContext();

        public UsersController(IUserAppService userAppService, RoleManager roleManager)
        {
            _userAppService = userAppService;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index()
        {
            var users = (await _userAppService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue })).Items; //Paging not implemented yet
            var roles = (await _userAppService.GetRoles()).Items;

            var eagerLoad = db.EntityForFirstDBs
                                .Include(x => x.EntityChildList.Select(c => c.UserMaster))
                                .Include(x => x.UserMaster)
                                .MapTo<List<EntityForFirstDBListDto>>();

            var model = new UserListViewModel
            {
                Users = users,
                Roles = roles,
                EntityForFirstDBList = eagerLoad
            };

            return View(model);
        }

        public async Task<ActionResult> EditUserModal(long userId)
        {
            var user = await _userAppService.Get(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return View("_EditUserModal", model);
        }
    }
}