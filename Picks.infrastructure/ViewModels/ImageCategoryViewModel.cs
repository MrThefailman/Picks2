using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.infrastructure.ViewModels
{
    public class ImageCategoryViewModel
    {
        public IEnumerable<ImageViewModel> Images { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
