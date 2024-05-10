using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data.GlobalEntities;

[Table("UserDepartment")]
public partial class UserDepartment
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string? UserId { get; set; }

    public int? DepartmentId { get; set; }
}
