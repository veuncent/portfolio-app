using PortfolioMyriam.Data;
using PortfolioMyriam.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public ProjectType? ProjectType { get; set; }

        public virtual ICollection<PortfolioItem> PortfolioItems { get; set; }

        public ProjectViewModel ToViewModel(ApplicationDbContext context)
        {
            var viewModel = new ProjectViewModel
            {
                Id = Id,
                Title = Title,
                Description = Description,
                ProjectType = ProjectType,
                Image = Image,
            };

            if (PortfolioItems != null)
            {
                viewModel.PortfolioItems = PortfolioItems.Select(pi => pi.ToViewModel(context));
            };

            return viewModel;
        }
    }
}
