using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

/// <summary>
/// 		
/// </summary>
[Table("authoritymaster")]
public partial class Authoritymaster
{
    [Key]
    public int Id { get; set; }

    [StringLength(500)]
    public string? AuthorityName { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }
}
