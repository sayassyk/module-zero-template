﻿using Abp.Domain.Entities.Auditing;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.FirstContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.FirstContext
{
    [Table("EntityForFirstDB")]
    public partial class EntityForFirstDB : AuditedEntity
    {
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }


        public long? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User UserMaster { get; set; }

        public virtual ICollection<EntityChild> EntityChildList { get; set; }

    }
}
