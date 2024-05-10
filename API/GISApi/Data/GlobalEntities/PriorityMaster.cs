using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("PriorityMaster")]
public partial class PriorityMaster
{
    [Key]
    public int PriorityId { get; set; }

    [StringLength(50)]
    public string Priority { get; set; } = null!;
}
