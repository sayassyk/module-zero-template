using Abp.Domain.Entities.Auditing;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.SecondContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.FirstContext
{
    public class EntityChild : AuditedEntity
    {
        public int MyChildProperty1 { get; set; }
        public int MyChildProperty2 { get; set; }

        public int EntityMasterTableId { get; set; }
        [ForeignKey("EntityMasterTableId")]
        public virtual EntityForFirstDB EntityForFirstDB { get; set; }

        public long? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User UserMaster { get; set; }
    }
}
