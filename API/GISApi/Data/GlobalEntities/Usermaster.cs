using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("usermaster")]
public partial class Usermaster
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(500)]
    public string? FirstName { get; set; }

    [StringLength(500)]
    public string? LastName { get; set; }

    [StringLength(500)]
    public string? EmailAddress { get; set; }

    [StringLength(500)]
    public string? PhoneNo { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public int? RoleId { get; set; }

    public int? DepartmentId { get; set; }

    [StringLength(500)]
    public string? Designation { get; set; }

    [StringLength(500)]
    public string? SystemDesignation { get; set; }

    public int? ReportedTo { get; set; }

    [StringLength(500)]
    public string? Password { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }

    [Column(TypeName = "date")]
    public DateTime? CreatedOn { get; set; }
}
