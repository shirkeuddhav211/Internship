using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("tblResult")]
public partial class TblResult
{
    [Key]
    public int ResultId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ResultStatus { get; set; }

    public int? DisplayOrder { get; set; }

    [InverseProperty("Result")]
    public virtual ICollection<TblInspection> TblInspections { get; set; } = new List<TblInspection>();
}
