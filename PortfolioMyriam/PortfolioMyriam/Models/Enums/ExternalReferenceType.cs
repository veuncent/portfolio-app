using System.ComponentModel.DataAnnotations;

namespace PortfolioMyriam.Models.Enums
{
    public enum ExternalReferenceType
    {
        [Display(Name = "IFrame")]
        IFrame = 0,
        [Display(Name = "URL")]
        URL = 1,
        [Display(Name = "Image")]
        Image = 2
    }
}
