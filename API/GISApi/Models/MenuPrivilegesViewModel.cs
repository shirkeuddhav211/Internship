using System.ComponentModel.DataAnnotations;

namespace GISApi.Models
{
    public class MenuPrivilegesViewModel
    {
        public int Id { get; set; }

        public int? OrganisationId { get; set; }

        public int? MenuId { get; set; }
        [StringLength(100, ErrorMessage = "MenuName Max Length is 100")]
        public string? MenuName{ get; set; }

        public int? FuncitonId { get; set; }

        public bool? IsVisible { get; set; }

        public bool? Add { get; set; }

        public bool? ReadOnly { get; set; }

        public bool? Modify { get; set; }

    }
}
