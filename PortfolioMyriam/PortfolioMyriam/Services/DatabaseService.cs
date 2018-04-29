using Microsoft.EntityFrameworkCore;
using PortfolioMyriam.Data;
using PortfolioMyriam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Services
{
    public class DatabaseService : IDatabaseService
    {
        private ApplicationDbContext _context;

        public DatabaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectBaseViewModel>> GetProjectBaseViewModelList()
        {
            return await _context.Projects.Select(p => new ProjectBaseViewModel
            {
                Id = p.Id,
                Title = p.Title
            }).ToListAsync();
        }

        public async Task<Project> GetProjectDetails(int id)
        {
            var portfolioItems = _context.PortfolioItem
                .Where(pi => pi.Project.Id == id)
                .OrderBy(pi => pi.ExternalReference.ExternalReferenceType)
                .ToList();

            return await _context.Projects
                .Select(p => new Project
                {
                    Id = p.Id,
                    Title = p.Title,
                    ProjectType = p.ProjectType,
                    Description = p.Description,
                    Image = p.Image,
                    PortfolioItems = portfolioItems
                })
                .SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
