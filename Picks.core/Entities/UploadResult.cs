using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.core.Entities
{
    public class UploadResult
    {
        public Category Category { get; set; }
        public List<Image> CreatedImages { get; set; }

        public UploadResult()
        {
            CreatedImages = new List<Image>();
        }
    }
}
