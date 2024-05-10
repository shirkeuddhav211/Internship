using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("UserResetPassword")]
public partial class UserResetPassword
{
    [Key]
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Flag { get; set; }
}
