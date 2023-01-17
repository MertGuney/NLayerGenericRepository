using System;
using System.Text.Json.Serialization;

namespace DefaultGenericProject.Core.DTOs
{
    public class BaseEntityDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? UpdatedDate { get; set; }
    }
}
