using System;
using System.Collections.Generic;
using System.Text;

namespace NewApp.Core.Models.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        
    }

    public class AuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
