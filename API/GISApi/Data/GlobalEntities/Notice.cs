using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("Notice")]
public partial class Notice
{
    [Key]
    public int Id { get; set; }

    [Column("Notice")]
    public string? Notice1 { get; set; }
}
