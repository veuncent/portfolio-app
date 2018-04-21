using PortfolioMyriam.Data;

namespace PortfolioMyriam.Models
{
    public class PortfolioItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ExternalReference ExternalReference { get; set; }

        public virtual Project Project { get; set; }

        public PortfolioItemViewModel ToViewModel(ApplicationDbContext context)
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

            if (Project != null)
            {
                viewModel.ProjectId = Project.Id;
            }

            return viewModel;
        }
    }
}
