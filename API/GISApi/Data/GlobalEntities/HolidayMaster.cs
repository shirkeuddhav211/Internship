﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("HolidayMaster")]
public partial class HolidayMaster
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HolidayDate { get; set; }

    public string? Description { get; set; }
}
