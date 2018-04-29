using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PortfolioMyriam.Data;
using PortfolioMyriam.Models.Enums;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Models
{
    public class ProjectViewModel : ProjectBaseViewModel
    {
        public string Description { get; set; }
        public ProjectType ProjectType { get; set; }
        public IFormFile ImageUpload { get; set; }
        public string ImageViewOnly { get; set; }
        public IEnumerable<PortfolioItemViewModel> PortfolioItems { get; set; }

        public async Task SaveModelAsync(ApplicationDbContext context)
        {
            var entity = new Project
            {
                Id = Id,
                Title = Title,
                Description = Description,
                ProjectType = ProjectType
            };

            if (ImageUpload != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageUpload.CopyToAsync(memoryStream);
                    entity.Image = memoryStream.ToArray();
                };
            }

            if (PortfolioItems != null)
            {
                entity.PortfolioItems = PortfolioItems.Select(pi => pi.ToEntity(context)).ToList();
            }

            context.Update(entity);
            context.Entry(entity).Property(p => p.Image).IsModified = ImageUpload != null ? true : false;

            await context.SaveChangesAsync();
        }
    }
}
