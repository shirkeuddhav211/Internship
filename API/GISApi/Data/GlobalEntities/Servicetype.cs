using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("servicetype")]
public partial class Servicetype
{
    [Key]
    public int Id { get; set; }

    [Column("ServiceType")]
    [StringLength(100)]
    public string? ServiceType1 { get; set; }
}
