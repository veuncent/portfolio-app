using PortfolioMyriam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Services
{
    public interface IDatabaseService
    {
        Task<IEnumerable<ProjectBaseViewModel>> GetProjectBaseViewModelList();
        Task<Project> GetProjectDetails(int id);
    }
}
