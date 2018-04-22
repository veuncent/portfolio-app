using PortfolioMyriam.Data;

namespace PortfolioMyriam.Models
{
    public class ProjectBaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual Project ToEntity(ApplicationDbContext context)
        {
            return new Project
            {
                Id = Id,
                Title = Title
            };
        }
    }
}
