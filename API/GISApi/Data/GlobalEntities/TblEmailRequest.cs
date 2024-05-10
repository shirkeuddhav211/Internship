using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("tblEmailRequest")]
public partial class TblEmailRequest
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string? EmailTo { get; set; }

    public string? EmailSubject { get; set; }

    public string? EmailBody { get; set; }

    [Column("EmailCC")]
    public string? EmailCc { get; set; }

    [Column("EmailBCC")]
    public string? EmailBcc { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    public int? AttemptNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastAttemptDateTime { get; set; }
}
