using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.DTOs.Roles
{
    public class UserRoleDTO : BaseEntityDTO
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
