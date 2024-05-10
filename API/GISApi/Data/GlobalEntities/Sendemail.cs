using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("sendemail")]
public partial class Sendemail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("from")]
    [StringLength(100)]
    public string? From { get; set; }

    [Column("to")]
    [StringLength(100)]
    public string? To { get; set; }

    [Column("cc")]
    [StringLength(100)]
    public string? Cc { get; set; }

    [Column("bcc")]
    [StringLength(100)]
    public string? Bcc { get; set; }

    [Column("subject")]
    [StringLength(256)]
    public string? Subject { get; set; }

    [Column("body", TypeName = "text")]
    public string? Body { get; set; }

    [Column("type")]
    [StringLength(45)]
    public string? Type { get; set; }

    [Column("issend")]
    public int? Issend { get; set; }
}
