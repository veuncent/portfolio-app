using PortfolioMyriam.Data;
using PortfolioMyriam.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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
                ProjectType = ProjectType
            };

            if (Image != null)
            {
                viewModel.ImageViewOnly = string.Format($"data:image/png;base64,{Convert.ToBase64String(Image)}");
            }

            if (PortfolioItems != null)
            {
                viewModel.PortfolioItems = PortfolioItems.Select(pi => pi.ToViewModel(context));
            };

            return viewModel;
        }
    }
}
