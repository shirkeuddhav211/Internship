using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class WireTypeViewModel
    {
        public int Id { get; set; }
        [MaxLength(500, ErrorMessage = "Amps must be less than 500 characters.")]
        public string Amps { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Type must be less than 500 characters.")]
        public string Type { get; set; }
        [Required]
        public decimal WireCost { get; set; }
        [Required]
        public decimal Standard { get; set; }
        [Required]
        public decimal Difficult { get; set; }
        [Required]
        public decimal VeryDifficult { get; set; }
        [Required]
        public decimal Terminations { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "GndWire must be less than 500 characters.")]
        public string GndWire { get; set; }
        [Required]
        public decimal GndCost { get; set; }
        [Required]
        public decimal Standard2 { get; set; }
        [Required]
        public decimal Difficult2 { get; set; }
        [Required]
        public decimal VeryDifficult2 { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "ConduitSize must be less than 500 characters.")]
        public string ConduitSize { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }

        public bool IsSelectStaus { get; set; }
    }
}
