using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("statusmaster")]
public partial class Statusmaster
{
    [Key]
    public int Id { get; set; }

    [StringLength(450)]
    public string? Status { get; set; }
}
