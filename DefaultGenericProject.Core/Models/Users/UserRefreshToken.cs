using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Models.Users
{
    public class UserRefreshToken : BaseEntity
    {
        public string Code { get; set; }
        public DateTime Expiration { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
