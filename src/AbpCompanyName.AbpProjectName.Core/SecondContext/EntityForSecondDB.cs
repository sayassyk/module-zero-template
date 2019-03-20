using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.SecondContext
{
    [Table("EntityForSecondDB")]
    public partial class EntityForSecondDB : AuditedEntity
    {
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
    }
}
