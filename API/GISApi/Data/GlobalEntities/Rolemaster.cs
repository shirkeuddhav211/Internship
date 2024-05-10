using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

/// <summary>
/// 		
/// </summary>
[Table("rolemaster")]
public partial class Rolemaster
{
    [Key]
    public int Id { get; set; }

    [StringLength(500)]
    public string? RoleName { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }

    [StringLength(500)]
    public string? RoleDescription { get; set; }
}
