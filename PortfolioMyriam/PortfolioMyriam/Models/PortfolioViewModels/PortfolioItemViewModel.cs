using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Models
{
    public class PortfolioItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ExternalReferenceViewModel ExternalReference { get; set; }

        public PortfolioItem ToEntity()
        {
            return new PortfolioItem
            {
                Id = this.Id,
                Title = Title,
                Description = Description,
                ExternalReference = new ExternalReference
                {
                    Id = ExternalReference.Id,
                    ExternalReferenceType = ExternalReference.ExternalReferenceType,
                    Uri = ExternalReference.Uri
                }
            };
        }
    }
}
