using PortfolioMyriam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Services
{
    public interface IDatabaseService
    {
        IEnumerable<ProjectBaseViewModel> GetProjectBaseViewModelList();
    }
}
