using PortfolioMyriam.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Models
{
    public class ExternalReferenceViewModel
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public ExternalReferenceType ExternalReferenceType { get; set; }

        public ExternalReference ToEntity()
        {
            return new ExternalReference
            {
                Id = Id,
                ExternalReferenceType = ExternalReferenceType,
                Uri = Uri
            };
        }
    }
}
