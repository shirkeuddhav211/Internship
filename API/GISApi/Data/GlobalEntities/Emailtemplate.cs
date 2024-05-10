using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("emailtemplate")]
public partial class Emailtemplate
{
    [Key]
    public int Id { get; set; }

    [Column("emailRef")]
    [StringLength(500)]
    public string? EmailRef { get; set; }

    [Column("inwardType")]
    [StringLength(500)]
    public string? InwardType { get; set; }

    [Column("inwardDate", TypeName = "datetime")]
    public DateTime? InwardDate { get; set; }

    [Column("regulatoryAuthority")]
    [StringLength(500)]
    public string? RegulatoryAuthority { get; set; }

    [Column("refNo")]
    [StringLength(500)]
    public string? RefNo { get; set; }

    [Column("emailSubject")]
    [StringLength(500)]
    public string? EmailSubject { get; set; }

    [Column("emailBody")]
    [StringLength(1500)]
    public string? EmailBody { get; set; }

    public int? OrganisationId { get; set; }
}
