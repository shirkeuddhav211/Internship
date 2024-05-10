using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("tblInspectionType")]
public partial class TblInspectionType
{
    [Key]
    public int InspectionTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? InspectionType { get; set; }

    [Required]
    public bool? IsActive { get; set; }
}
