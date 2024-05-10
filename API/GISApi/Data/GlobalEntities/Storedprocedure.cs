using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("storedprocedures")]
public partial class Storedprocedure
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }
}
