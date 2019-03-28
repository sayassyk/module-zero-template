using Abp.Application.Services.Dto;
using AbpCompanyName.AbpProjectName.FirstContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbpCompanyName.AbpProjectName.EntityModels
{
    public class EntityForFirstDBListDto : EntityDto
    {
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
        public long? CreatedBy { get; set; }

        public int MyChildProperty1 { get; set; }
        public string MasterCreatedUserName { get; set; }        
        public string ChildCreatedUserName { get; set; }        
    }
}