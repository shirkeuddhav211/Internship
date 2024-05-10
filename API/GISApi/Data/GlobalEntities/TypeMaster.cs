using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("TypeMaster")]
public partial class TypeMaster
{
    [Key]
    public int TypeId { get; set; }

    [StringLength(50)]
    public string Type { get; set; } = null!;
}
