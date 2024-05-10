using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Keyless]
[Table("tblInspectionAttribute")]
public partial class TblInspectionAttribute
{
    [Column("InsAtrID")]
    public int InsAtrId { get; set; }

    [Column("InspectionID")]
    public int InspectionId { get; set; }

    [Column("AtrID")]
    public int AtrId { get; set; }

    [StringLength(250)]
    public string? Comments { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }
}
