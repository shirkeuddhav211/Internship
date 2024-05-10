using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class ApplicationRole : IdentityRole
    {
        [MaxLength(200)]
        public string RoleAction { get; set; }
    }
}
