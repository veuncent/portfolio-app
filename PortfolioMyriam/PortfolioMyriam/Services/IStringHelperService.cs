using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Services
{
    public interface IStringHelperService
    {
        string GetBase64EncodedString(string plainText);
    }
}
