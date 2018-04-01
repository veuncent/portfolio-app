using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Models
{
    public class PortfolioItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ExternalReference ExternalReference { get; set; }

        public PortfolioItemViewModel ToViewModel()
        {
            var viewModel = new PortfolioItemViewModel
            {
                Id = Id,
                Title = Title,
                Description = Description
            };

            if (ExternalReference != null)
            {
                viewModel.ExternalReference = new ExternalReferenceViewModel
                {
                    Id = ExternalReference.Id,
                    ExternalReferenceType = ExternalReference.ExternalReferenceType,
                    Uri = ExternalReference.Uri ?? null
                };
            }

            return viewModel;
        }
    }
}
