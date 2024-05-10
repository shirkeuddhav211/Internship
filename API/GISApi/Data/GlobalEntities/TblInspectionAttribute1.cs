using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("tblInspectionAttributes")]
public partial class TblInspectionAttribute1
{
    [Key]
    public int Id { get; set; }

    public int AttrId { get; set; }

    [Column("InspectionID")]
    public int InspectionId { get; set; }

    [StringLength(500)]
    public string Comments { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [ForeignKey("AttrId")]
    [InverseProperty("TblInspectionAttribute1s")]
    public virtual TblAttribute Attr { get; set; } = null!;
}
