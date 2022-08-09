using DefaultGenericProject.Core.Enums;
using System;

namespace DefaultGenericProject.Core.Models
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DataStatus Status { get; set; } = DataStatus.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
