using PortfolioMyriam.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PortfolioMyriam.Models
{
    public class ExternalReference
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        [EnumDataType(typeof(ExternalReferenceType))]
        public ExternalReferenceType ExternalReferenceType { get; set; }

        public virtual PortfolioItem PortfolioItem { get; set; }
    }
}
