using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("tblAttributes")]
public partial class TblAttribute
{
    [Key]
    public int AttrId { get; set; }

    [StringLength(255)]
    public string AttributeDesc { get; set; } = null!;

    [InverseProperty("Attr")]
    public virtual ICollection<TblInspectionAttribute1> TblInspectionAttribute1s { get; set; } = new List<TblInspectionAttribute1>();
}
