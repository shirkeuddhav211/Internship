using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

public partial class InspectionType
{
    [Key]
    public int Id { get; set; }

    public string? InspectionTypeName { get; set; }

    public bool? IsActive { get; set; }
}
