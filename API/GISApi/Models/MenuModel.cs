using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public string Type { get; set; }
        public int SubMenuId { get; set; }
        public string SubType { get; set; }
        public int ParentId { get; set; }
    }
}
