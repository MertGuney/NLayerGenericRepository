using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.DTOs
{
    public class ProductDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public int Stock  { get; set; }
    }
}
