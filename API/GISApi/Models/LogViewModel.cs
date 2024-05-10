using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class LogViewModel
    {
        public int LogId { get; set; }
        public string LogInStamp { get; set; }
        public DateTime? LogInDate { get; set; }
        public DateTime? LogInTime { get; set; }
        public string Username { get; set; }
        public string Ipaddress { get; set; }
        public string OtherInfo { get; set; }
        public string LogOutDate { get; set; }
        public string LogOutTime { get; set; }
    }
}
