using PortfolioMyriam.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PortfolioMyriam.Models
{
    public class ExternalReferenceViewModel
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        [Display(Name = "External Reference Type")]
        public ExternalReferenceType ExternalReferenceType { get; set; }

        public ExternalReference ToEntity()
        {
            return new ExternalReference
            {
                Id = Id,
                ExternalReferenceType = ExternalReferenceType,
                Uri = Uri
            };
        }
    }
}
