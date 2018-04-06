using PortfolioMyriam.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Models
{
    public class ExternalReference
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        [EnumDataType(typeof(ExternalReferenceType))]
        public ExternalReferenceType ExternalReferenceType { get; set; }

        public virtual PortfolioItem PortfolioItem { get; set; }
    }
}
