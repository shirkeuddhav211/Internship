using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Keyless]
public partial class AspNetUserToken
{
    [StringLength(128)]
    public string UserId { get; set; } = null!;

    [StringLength(450)]
    public string LoginProvider { get; set; } = null!;

    [StringLength(450)]
    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    [ForeignKey("UserId")]
    public virtual AspNetUser User { get; set; } = null!;
}
