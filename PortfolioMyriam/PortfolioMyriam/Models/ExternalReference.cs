using PortfolioMyriam.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioMyriam.Models
{
    public class ExternalReference
    {
        [ForeignKey("PortfolioItem")]
        public int Id { get; set; }
        public string Uri { get; set; }
        [EnumDataType(typeof(ExternalReferenceType))]
        public ExternalReferenceType ExternalReferenceType { get; set; }

        public PortfolioItem PortfolioItem { get; set; }
    }
}
