using PortfolioMyriam.Data;
using System.Threading.Tasks;

namespace PortfolioMyriam.Models
{
    public class ProjectBaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual async Task<Project> ToEntityAsync(ApplicationDbContext context)
        {
            return new Project
            {
                Id = Id,
                Title = Title
            };
        }
    }
}
