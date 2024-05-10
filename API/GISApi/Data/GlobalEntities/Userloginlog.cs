using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("userloginlogs")]
public partial class Userloginlog
{
    [Key]
    public int Id { get; set; }

    [Column("userId")]
    [StringLength(500)]
    public string? UserId { get; set; }

    [Column("type")]
    [StringLength(45)]
    public string? Type { get; set; }

    [Column("logindatetime", TypeName = "datetime")]
    public DateTime? Logindatetime { get; set; }

    [Column("logoutdatetime", TypeName = "datetime")]
    public DateTime? Logoutdatetime { get; set; }

    [Column("ipaddress")]
    [StringLength(500)]
    public string? Ipaddress { get; set; }
}
