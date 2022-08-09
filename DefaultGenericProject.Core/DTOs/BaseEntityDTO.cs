using DefaultGenericProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.DTOs
{
    public class BaseEntityDTO
    {
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonIgnore]
        public DataStatus Status { get; set; } = DataStatus.Active;
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? UpdatedDate { get; set; }
    }
}
