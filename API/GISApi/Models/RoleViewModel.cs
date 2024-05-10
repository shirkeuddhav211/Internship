using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class RoleViewModel
    {
        [StringLength(100, ErrorMessage = "Id Max Length is 100")]

        public string Id { get; set; }
        [StringLength(100, ErrorMessage = "Name Max Length is 100")]
        public string Name { get; set; }

    }
}
