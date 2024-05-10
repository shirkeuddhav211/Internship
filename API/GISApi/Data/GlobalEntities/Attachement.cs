using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("attachements")]
public partial class Attachement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(45)]
    public string AttachementsFor { get; set; } = null!;

    public int AttachmentTypeId { get; set; }

    [StringLength(100)]
    public string FileName { get; set; } = null!;

    [StringLength(1000)]
    public string FilePath { get; set; } = null!;

    [StringLength(200)]
    public string? Description { get; set; }

    public int? OrganisationId { get; set; }
}
