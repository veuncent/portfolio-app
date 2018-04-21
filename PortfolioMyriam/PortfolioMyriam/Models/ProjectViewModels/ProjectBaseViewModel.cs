using PortfolioMyriam.Data;
using System.Collections.Generic;
using System.Linq;

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
