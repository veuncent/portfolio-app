﻿using PortfolioMyriam.Data;
using PortfolioMyriam.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioMyriam.Models
{
    public class ProjectViewModel : ProjectBaseViewModel
    {
        public string Description { get; set; }
        public ProjectType? ProjectType { get; set; }
        public byte[] Image { get; set; }
        public IEnumerable<PortfolioItemViewModel> PortfolioItems { get; set; }

        public override Project ToEntity(ApplicationDbContext context)
        {
            var entity = new Project
            {
                Id = Id,
                Title = Title,
                Description = Description,
                ProjectType = ProjectType
            };

            if (Image != null)
            {
                entity.Image = Image;
            };

            if (PortfolioItems != null)
            {
                entity.PortfolioItems = PortfolioItems.Select(pi => pi.ToEntity(context)).ToList();
            };

            return entity;
        }
    }
}