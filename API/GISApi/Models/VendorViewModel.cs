using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class VendorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Division { get; set; }
        public string BillTo { get; set; }
        public string PrimaryContact { get; set; }
        public string MainPhone { get; set; }
        public List<VendoremailcontactViewModel> vendorEmailContactViewModels { get; set; } = new List<VendoremailcontactViewModel>();

    }
}
