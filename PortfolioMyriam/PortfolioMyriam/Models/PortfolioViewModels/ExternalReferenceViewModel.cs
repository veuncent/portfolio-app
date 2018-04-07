using PortfolioMyriam.Models.Enums;

namespace PortfolioMyriam.Models
{
    public class ExternalReferenceViewModel
    {
        public int Id { get; set; }
        public string Uri { get; set; }
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
