using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GISApi.Models
{
    public class InspctionViewModel
    {
        public int Id { get; set; }
        
        public string? UserId { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Zip { get; set; }

        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        public string? Alias { get; set; }

        public string? InspectionType1 { get; set; }

        public string? InspectionType2 { get; set; }

        public string? InspectionType3 { get; set; }

        public string? InspectionType4 { get; set; }

        public string? Status1 { get; set; }

        public string? Status2 { get; set; }

        public string? Status3 { get; set; }

        public string? Status4 { get; set; }

        public string? Email { get; set; }

        public string? PermitNo { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? InspectionDate { get; set; }

        public string AmPm { get; set; }

        public int? InspectionTypeId { get; set; }

        public string? Comments { get; set; }

        public string? Contact { get; set; }

        public string Type { get; set; }

        public int? InspectionOrder { get; set; }

        public string? CreatedBy { get; set; }
       
        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

       
        public DateTime? UpdatedDate { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? ResidentUserId { get; set; }

        public bool? Acknowledge { get; set; }

        public bool? IsCancelled { get; set; }

        public bool? IsRejected { get; set; }
        public string InspectorName { get; set; }
        public string? InspectionStatus { get; set; }
        public string? Apartment { get; set; }
        public string RejectComments { get; set; }
    }
}
