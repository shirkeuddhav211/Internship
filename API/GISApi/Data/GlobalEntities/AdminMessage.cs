using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

public partial class AdminMessage
{
    [Key]
    public int Id { get; set; }

    public string? Message { get; set; }
}
