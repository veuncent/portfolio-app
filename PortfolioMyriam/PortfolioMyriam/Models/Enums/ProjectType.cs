using System.ComponentModel.DataAnnotations;

namespace PortfolioMyriam.Models.Enums
{
    public enum ProjectType
    {
        [Display(Name = "Cultural History" )]
        CulturalHistory = 0,
        [Display(Name = "Natural History" )]
        NaturalHistory = 1,
        [Display(Name = "Miscellaneous")]
        Miscellaneous = 2
    }
}
