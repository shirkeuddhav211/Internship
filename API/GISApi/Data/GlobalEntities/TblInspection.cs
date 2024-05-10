using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("tblInspection")]
public partial class TblInspection
{
    [Key]
    public int InspectionId { get; set; }

    public int? UserId { get; set; }

    [Column("Address Line1")]
    [StringLength(50)]
    public string? AddressLine1 { get; set; }

    [Column("Address Line2")]
    [StringLength(50)]
    public string? AddressLine2 { get; set; }

    [StringLength(25)]
    public string? City { get; set; }

    [StringLength(25)]
    public string? State { get; set; }

    public int? Zip { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(25)]
    public string? PermitNo { get; set; }

    public bool? Type { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InspectionDate { get; set; }

    [Column("AM_PM")]
    public bool? AmPm { get; set; }

    public int? InspectionTypeId1 { get; set; }

    public int? InspectionTypeId2 { get; set; }

    public int? InspectionTypeId3 { get; set; }

    public int? Status1 { get; set; }

    public int? Status2 { get; set; }

    public int? Status3 { get; set; }

    public int? InspectionOrder { get; set; }

    public int? ResultId { get; set; }

    [StringLength(255)]
    public string? Comments { get; set; }

    [StringLength(50)]
    public string? Contact { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(50)]
    public string? Longitude { get; set; }

    [Column("latitude")]
    [StringLength(50)]
    public string? Latitude { get; set; }

    [Column("formattedAddress")]
    [StringLength(200)]
    public string? FormattedAddress { get; set; }

    public bool? Acknowledge { get; set; }

    public int? ResidentUserId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    public bool? IsCancelled { get; set; }

    [StringLength(100)]
    public string? InspectionType4 { get; set; }

    public int? Status4 { get; set; }

    [ForeignKey("ResultId")]
    [InverseProperty("TblInspections")]
    public virtual TblResult? Result { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TblInspections")]
    public virtual TblLoginInfo? User { get; set; }
}
