using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("consequencesmaster")]
public partial class Consequencesmaster
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(45)]
    public string ConsequencesName { get; set; } = null!;

    public int? OrganisationId { get; set; }
}
