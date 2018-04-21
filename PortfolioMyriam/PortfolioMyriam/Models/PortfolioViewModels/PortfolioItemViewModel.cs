using PortfolioMyriam.Data;
using System.Collections.Generic;

namespace PortfolioMyriam.Models
{
    public class PortfolioItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ExternalReferenceViewModel ExternalReference { get; set; }
        public int ProjectId { get; set; }
        public IEnumerable<ProjectBaseViewModel> ProjectOptions { get; set; }

        public PortfolioItem ToEntity(ApplicationDbContext context)
        {
            var entity = new PortfolioItem
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

            if (ProjectId != 0)
            {
                var project = context.Projects.FindAsync(ProjectId);
                project.Wait();
                entity.Project = project.Result;
            }

            return entity;
        }
    }
}
