using Picks.core.Entities;
using Picks.infrastructure.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.infrastructure.ViewModels
{
    public class AddImageViewModel
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public string BannerClass { get; set; }
        public string BannerText { get; set; }
    }
}
