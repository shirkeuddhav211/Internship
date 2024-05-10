using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("regulatoryauthoritymaster")]
public partial class Regulatoryauthoritymaster
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(20)]
    public string AuthorityName { get; set; } = null!;
}
