using DefaultGenericProject.Core.Enums;
using System;

namespace DefaultGenericProject.Core.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DataStatus Status { get; set; } = DataStatus.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
