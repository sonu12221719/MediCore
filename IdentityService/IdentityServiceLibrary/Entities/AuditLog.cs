using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IdentityServiceLibrary.Entities
{
    public class AuditLog
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
