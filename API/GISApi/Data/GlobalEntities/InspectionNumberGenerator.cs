using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("InspectionNumberGenerator")]
public partial class InspectionNumberGenerator
{
    [Key]
    public int Id { get; set; }

    public int? InspectionStartId { get; set; }

    public int? InspetionNextId { get; set; }
}
