using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.DTOs.Roles
{
    public class UserRoleDTO : BaseEntityDTO
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
