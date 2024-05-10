using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class RouteViewModel
    {
        public string path { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string className { get; set; }
        public bool extralink { get; set; }
        public List<RouteViewModel> submenu { get; set; } = new List<RouteViewModel>();
        public int index { get; set; }
    }
}
