using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("tblLoginInfo")]
public partial class TblLoginInfo
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string? UserName { get; set; }

    [StringLength(20)]
    public string? Password { get; set; }

    [StringLength(50)]
    public string? FirsName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string? Department { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Role { get; set; }

    [StringLength(20)]
    public string? Alias { get; set; }

    [StringLength(25)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("Address_Line1")]
    [StringLength(50)]
    public string? AddressLine1 { get; set; }

    [Column("Address_Line2")]
    [StringLength(50)]
    public string? AddressLine2 { get; set; }

    [StringLength(25)]
    public string? City { get; set; }

    [StringLength(25)]
    public string? State { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TblInspection> TblInspections { get; set; } = new List<TblInspection>();
}
